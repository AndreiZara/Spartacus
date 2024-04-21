using AutoMapper;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System;
using System.Linq;
using System.Net.Http.Headers;
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
        //internal UProfData UserProfileAction(int id)
        //{
        //    UProfData userProfile;
        //    using(var debil = new UserContext())
        //    {
        //        var user = debil.Users.FirstOrDefault(u => u.Id == id);
        //        var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UProfData>());
        //        userProfile = config.CreateMapper().Map<UProfData>(user);
        //    }
        //    using(var debil = new MembershipContext())
        //    {
        //        var membership = debil.Memberships.FirstOrDefault(u => u.Id == userProfile.MembershipId);
        //        var config = new MapperConfiguration(cfg => cfg.CreateMap<MsTable, UProfData>());
        //        userProfile = config.CreateMapper().Map<UProfData>(membership);
        //    }

        //    return userProfile;
        //}

        internal HttpCookie GetCookieAction(string data)
        {
            var cookie = new HttpCookie("UserCookie", data);

            using (var debil = new SessionContext())
            {
                Session current;
                current = debil.Sessions.FirstOrDefault(s => s.Username == data);

                if (current != null)
                {
                    current.CookieString = cookie.Value;
                    current.ExpireTime = DateTime.Now.AddMinutes(60);
                }
                else
                {
                    debil.Sessions.Add(new Session()
                    {
                        Username = data,
                        CookieString = cookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(60)
                    });
                }
                debil.SaveChanges();
            }

            return cookie;
        }
        internal UProfData GetUserByCookieAction(string cookie)
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
            
            MsTable membership;
            using (var debil = new MembershipContext())
            {
                //membership = debil.Memberships.FirstOrDefault(u => u.Id == user.MembershipId);
                membership = null;
            }
            if (membership == null) return null;
            
            UProfData userProfile;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UProfData>());
            userProfile = config.CreateMapper().Map<UProfData>(user);
            
            config = new MapperConfiguration(cfg => cfg.CreateMap<MsTable, UProfData>());
            userProfile = config.CreateMapper().Map<UProfData>(membership);

            return userProfile;
        }
    }
}
