using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Spartacus.Web.Models
{
    public class UserTable
    {
        UserLogin a = new UserLogin();

        public List<UserLogin> UserList { get; set; }

        public void AddUser(UserLogin user)
        {
            UserList.Add(user);
        }

        public List<UserLogin> GetUsers()
        {
            return UserList;
        }

    }
}
