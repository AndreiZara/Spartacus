using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class UserMgmtApi
    {
        internal bool AddUserAction(UTable data)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(u => u.Username == data.Username);
                if (user != null) return false;

                debil.Users.Add(data);
                debil.SaveChanges();
            }
            return true;
        }

        internal List<UTable> GetUsersAction()
        {
            List<UTable> users;
            using (var debil = new UserContext())
            {
                users = debil.Users.Include(u => u.Membership).ToList();
            }
            return users;
        }

        internal UTable GetUserByIdAction(int id)
        {
            UTable user;
            using (var debil = new UserContext())
            {
                user = debil.Users.SingleOrDefault(u => u.Id == id);
            }
            return user;
        }

        internal bool UpdateUserAction(UTable data)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.FirstOrDefault(x => x.Id == data.Id);

                if (user == null) return false;

                user.Username = data.Username;
                user.Firstname = data.Firstname;
                user.Lastname = data.Lastname;
                user.Email = data.Email;
                user.Password = data.Password;
                user.LastIp = data.LastIp;
                user.LastLogin = data.LastLogin;
                user.Level = data.Level;

                debil.SaveChanges();
            }
            return true;

        }

        internal bool DeleteUserByIdAction(int id)
        {
            using (var debil = new UserContext())
            {
                var user = debil.Users.Find(id);
                if (user == null) return false;
                debil.Users.Remove(user);
                debil.SaveChanges();
            }
            return true;
        }
    }
}
