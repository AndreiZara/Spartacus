using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class UserApi
    {
        internal bool UserLoginAction(ULoginData data)
        {
            UDbTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.FirstOrDefault(u => u.Username == data.Name && u.Password == data.Password);
            }

            return user != null;
        }
        internal bool UserRegAction(URegData data)
        {
            UDbTable user;
            using (var debil = new UserContext())
            {
                // check if current username is not already taken
                user = debil.Users.FirstOrDefault(u => u.Username == data.Username);
                if (user != null) return false;

                debil.Users.Add(new UDbTable
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
    }
}
