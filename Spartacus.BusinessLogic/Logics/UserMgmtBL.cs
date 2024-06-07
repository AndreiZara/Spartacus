using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using System.Collections.Generic;
using System.Web;

namespace Spartacus.BusinessLogic.Logics
{
    public class UserMgmtBL : UserMgmtApi, IUserMgmt
    {
        public bool AddUser(UTable data) => AddUserAction(data);
        public bool DeleteUserById(int id) => DeleteUserByIdAction(id);
        public UTable GetUserById(int id) => GetUserByIdAction(id);
        public List<UTable> GetUsers() => GetUsersAction();
        public void RemoveUnconfirmedUsers() => RemoveUnconfirmedUsersAction();
        public SaveProfResp UpdateUser(UProfData data, HttpPostedFileBase image) => UpdateUserAction(data, image);
    }
}
