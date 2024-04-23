using AutoMapper;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using Spartacus.Helpers;
using System;
using System.Linq;
using System.Web;

namespace Spartacus.BusinessLogic.Core
{
    public class UserApi
    {
        internal bool UserLoginAction(ULoginData data)
        {
            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.FirstOrDefault(u => u.Username == data.Name && u.Password == data.Password);
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
    }
}
