using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Logics
{
    public class AdminBL : AdminApi, IAdmin
    {
        public void AddUser(UTable data)
        {
            AddUserAction(data);
        }

        public bool DeleteUserById(int id)
        {
            return DeleteUserByIdAction(id);
        }

        public UTable GetUserById(int id)
        {
            return GetUserByIdAction(id);
        }

        public List<UTable> GetUsers()
        {
            return GetUsersAction();
        }

        public bool UpdateUser(UTable data)
        {
            return UpdateUserAction(data);
        }
    }
}
