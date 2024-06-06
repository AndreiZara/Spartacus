using AutoMapper;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Helpers;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;

namespace Spartacus.BusinessLogic.Core
{
    public class SessionApi
    {
        internal bool UserLoginAction(ULoginData data)
        {
            using var debil = new UserContext();
            var user = debil.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);
            if (user != null)
            {
                user.LastLogin = data.LoginTime;
                user.LastIp = data.Ip;
                debil.SaveChanges();
            }
            return user != null;

        }

        internal bool UserRegAction(URegData data)
        {
            using var debil = new UserContext();
            // check if current username is not already taken
            var user = debil.Users.FirstOrDefault(u => u.Username == data.Username);
            if (user != null) return false;

            debil.Users.Add(new UTable
            {
                Username = data.Username,
                Firstname = data.Firstname,
                Lastname = data.Lastname,
                Password = data.Password,
                Email = data.Email,
                LastIp = data.Ip,
                LastLogin = DateTime.Now,
                Role = URole.Client
            });
            debil.SaveChanges();

            return true;
        }

        internal HttpCookie GetCookieAction(string data)
        {
            // better to store a string rather than int Id
            var cookie = new HttpCookie("UserCookie")
            {
                Value = CookieGenerator.EncryptStringAes(data, WebConfigurationManager.AppSettings["CookieAesShared"])
            };

            using (var debil = new SessionContext())
            {
                Session current;
                current = debil.Sessions.FirstOrDefault(s => s.Username == data);
                int sessionLength = 30;

                if (current != null)
                {
                    current.CookieString = cookie.Value;
                    current.ExpireTime = DateTime.Now.AddMinutes(sessionLength);
                }
                else
                {
                    debil.Sessions.Add(new Session()
                    {
                        Username = data,
                        CookieString = cookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(sessionLength)
                    });
                }
                debil.SaveChanges();
            }

            return cookie;
        }

        internal UserMinimal GetUserByCookieAction(string cookie)
        {
            Session current;
            using (var debil = new SessionContext())
            {
                current = debil.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }
            if (current == null) return null;

            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.FirstOrDefault(u => u.Username == current.Username);
            }
            if (user == null) return null;

            UserMinimal userMin;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UserMinimal>());
            userMin = config.CreateMapper().Map<UserMinimal>(user);

            return userMin;
        }

        internal UProfData GetProfileByCookieAction(string cookie)
        {
            Session current;
            using (var debil = new SessionContext())
            {
                current = debil.Sessions.FirstOrDefault(s => s.CookieString == cookie && s.ExpireTime > DateTime.Now);
            }
            if (current == null) return null;

            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.Include(u => u.Membership).Include(u => u.Trainer).FirstOrDefault(u => u.Username == current.Username);
                if (user == null) return null;

                UProfData userProf;
                // ignore the password
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UProfData>().ForMember(up => up.Password, opt => opt.Ignore()));
                userProf = config.CreateMapper().Map<UProfData>(user);

                if (user.Membership != null)
                {
                    userProf.StartTime = user.Membership.StartTime;
                    userProf.EndTime = user.Membership.EndTime;
                }

                userProf.Activity = user.Trainer?.Activity;
                userProf.Bio = user.Trainer?.Bio;
                userProf.InstagramUrl = user.Trainer?.InstagramUrl;
                userProf.FacebookUrl = user.Trainer?.FacebookUrl;

                return userProf;
            }
        }

        internal SaveProfResp SaveProfileByCookieAction(string cookie, UProfData data)
        {
            using var debil = new SessionContext();
            var current = debil.Sessions.FirstOrDefault(s => s.CookieString == cookie);
            if (current == null) return SaveProfResp.Failed;

            using var debil1 = new UserContext();
            var user = debil1.Users.FirstOrDefault(u => u.Username == current.Username && u.Password == data.Password);
            if (user == null) return SaveProfResp.Failed;

            if (data.Username != user.Username)
            {
                var userExists = debil1.Users.FirstOrDefault(u => u.Username == data.Username) != null;
                if (!userExists && (user.LastUsernameChange == null || user.LastUsernameChange.Value.AddDays(30) < DateTime.Now))
                {
                    user.LastUsernameChange = DateTime.Now;
                    user.Username = data.Username;
                    debil.Sessions.Remove(current);
                    debil.SaveChanges();
                }
                else return SaveProfResp.FailedUsername;
            }

            if (data.Image != null)
            {
                var newFileName = MediaHelper.SaveImageByUser(data.Image, user);
                if (newFileName == null) return SaveProfResp.FailedImage;
                user.FileName = newFileName;
            }

            if (user.Role == URole.Trainer)
            {
                if (user.Trainer == null)
                {
                    user.Trainer = new Domain.Entities.Trainer.TDTable
                    {
                        Activity = data.Activity,
                        Bio = data.Bio,
                        InstagramUrl = data.InstagramUrl,
                        FacebookUrl = data.FacebookUrl
                    };
                }
                else
                {
                    user.Trainer.Activity = data.Activity;
                    user.Trainer.Bio = data.Bio;
                    user.Trainer.InstagramUrl = data.InstagramUrl;
                    user.Trainer.FacebookUrl = data.FacebookUrl;
                }
            }

            // ignore FileName as it can be null if no new photo is specified
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UProfData, UTable>().ForMember(u => u.FileName, opt => opt.Ignore()));
            config.CreateMapper().Map(data, user);
            debil1.SaveChanges();

            return SaveProfResp.Success;
        }

        internal bool SetMsDurationForAction(string userCookie, MsDuration duration)
        {
            using var debil = new SessionContext();
            var session = debil.Sessions.FirstOrDefault(s => s.CookieString == userCookie && s.ExpireTime > DateTime.Now);
            if (session == null) return false;

            session.Duration = duration;
            debil.SaveChanges();
            return true;
        }

        internal MsDuration? GetMsDurationForAction(string userCookie)
        {
            using var debil = new SessionContext();
            var session = debil.Sessions.FirstOrDefault(s => s.CookieString == userCookie && s.ExpireTime > DateTime.Now);
            if (session == null) return null;
            return session.Duration;
        }

        internal bool AddMembershipForAction(string username, int? catId, MsDuration? duration)
        {
            if (duration == null || catId == null) return false;

            using var debil = new UserContext();
            var user = debil.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;

            if (user.Membership == null)
            {
                user.Membership = new Domain.Entities.Membership.MsTable
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddMonths((int)duration),
                };
            }
            else if (user.Membership.EndTime < DateTime.Now)
            {
                user.Membership.StartTime = DateTime.Now;
                user.Membership.EndTime = DateTime.Now.AddMonths((int)duration);
            }
            else return false;

            user.CatId = catId;
            user.Period = duration;
            debil.SaveChanges();
            return true;
        }

        internal string GetQrByIdAction(int id)
        {
            using var debil = new TokenContext();
            string value;
            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[12]; // Use a multiple of 3 (e.g. 3, 6, 12) to prevent output with trailing padding '=' characters in Base64).
                rng.GetBytes(bytes);

                // The `.Replace()` methods convert the Base64 string returned from `ToBase64String` to Base64Url.
                value = Convert.ToBase64String(bytes).Replace('+', '-').Replace('/', '_');
            }

            var token = debil.AccessTokens.FirstOrDefault(u => u.UserId == id);
            if (token != null)
            {
                token.EndTime = DateTime.Now.AddMinutes(2);
                token.Value = LoginHelpers.HashGen(value);
            }
            else
            {
                debil.AccessTokens.Add(new Domain.Entities.Tokens.AccessToken
                {
                    UserId = id,
                    Value = LoginHelpers.HashGen(value),
                    EndTime = DateTime.Now.AddMinutes(2)
                });
            }
            debil.SaveChanges();
            return value;
        }
    }
}
