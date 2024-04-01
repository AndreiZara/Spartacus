using System.Collections.Generic;


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
