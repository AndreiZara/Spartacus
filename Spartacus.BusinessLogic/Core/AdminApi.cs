using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Core
{
    public class AdminApi
    {
        public void AddUser(UDbTable user)
        {
            using (var debil = new UserContext())
            {
                debil.Users.Add(user);
                debil.SaveChanges();
            }
        }

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

        public bool UpdateUser(UDbTable user, int Id)
        {
            using (var debil = new UserContext())
            {
                var data = debil.Users.FirstOrDefault(x => x.Id == user.Id);

                if (data != null)
                {
                    data.Username = user.Username;
                    data.Password = user.Password;
                    data.Email = user.Email;
                    data.LastLogin = user.LastLogin;
                    data.LasIp = user.LasIp;
                    data.Id = user.Id;
                    data.Level = user.Level;
                    debil.SaveChanges();

                    return true;
                }

            }

            return false;
        }

    }
}

/*
 
 fnjsdfks dsjfksdf dkfksdf kdsf dsfksd fsd fksd f

 
 
 
 
 
 
 */