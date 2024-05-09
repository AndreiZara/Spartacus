using AutoMapper;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Helpers;
using System;
using System.Linq;
using System.Web;

namespace Spartacus.BusinessLogic.Core
{
    public class SessionApi
    {
        internal bool UserLoginAction(ULoginData data)
        {
            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);
            }

            return user != null;
        }
        internal bool UserRegAction(URegData data)
        {
            UTable user;
            using (var debil = new UserContext())
            {
                // check if current username is not already taken
                user = debil.Users.FirstOrDefault(u => u.Username == data.Username);
                if (user != null) return false;

                debil.Users.Add(new UTable
                {
                    Username = data.Username,
                    Firstname = data.Firstname,
                    Lastname = data.Lastname,
                    Password = data.Password,
                    Email = data.Email,
                    LastIp = data.Ip,
                    LastLogin = data.LoginDateTime,
                    Level = Domain.Enums.URole.Client
                });
                debil.SaveChanges();
            }

            return true;
        }

        internal HttpCookie GetCookieAction(string data)
        {
            // better to store a string rather than int Id
            var cookie = new HttpCookie("UserCookie")
            {
                Value = CookieGenerator.Create(data)
            };

            using (var debil = new SessionContext())
            {
                Session current;
                current = debil.Sessions.FirstOrDefault(s => s.Username == data);
                int sessionLength = 2;

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

            UserMinimal userProfile;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UserMinimal>());
            userProfile = config.CreateMapper().Map<UserMinimal>(user);

            return userProfile;
        }

        internal bool SetMsDurationForAction(string userCookie, MsDuration duration)
        {
            Session session;
            using (var debil = new SessionContext())
            {
                session = debil.Sessions.FirstOrDefault(s => s.CookieString == userCookie && s.ExpireTime > DateTime.Now);
                if (session == null) return false;

                session.Duration = duration;
                debil.SaveChanges();
            }
            return true;
        }

        internal MsDuration? GetMsDurationForAction(string userCookie)
        {
            Session session;
            using (var debil = new SessionContext())
            {
                session = debil.Sessions.FirstOrDefault(s => s.CookieString == userCookie && s.ExpireTime > DateTime.Now);
                if (session == null) return null;
            }
            return session.Duration;
        }

        internal bool AddMembershipForAction(string username, MsDuration? duration)
        {
            if (duration == null) return false;

            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.FirstOrDefault(u => u.Username == username);

                if (user.Membership == null)
                {
                    user.Membership = new Domain.Entities.Membership.MsTable
                    {
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now.AddMonths((int)duration),
                    };
                }
                else
                {
                    user.Membership.StartTime = DateTime.Now;
                    user.Membership.EndTime = DateTime.Now.AddMonths((int)duration);
                }
                debil.SaveChanges();
            }
            return true;
        }
    }
}
