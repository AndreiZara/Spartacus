using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class AdminApi
    {
        public List<UDbTable> GetUsersAction()
        {
            var users = new List<UDbTable>();
            using (var debil = new UserContext())
            {
                users = debil.Users.ToList();
            }

            return users;
        }

        public UDbTable GetUserByIdAction(int id)
        {
            var user = new UDbTable();
            using (var debil = new UserContext())
            {
                user = debil.Users.FirstOrDefault(u => u.Id == id);
            }
            return user;
        }

        public bool DeleteUserByIdAction(int id)
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
