using Spartacus.Domain.Entities.Trainer;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using System.Collections.Generic;
using System.Web;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IUserMgmt
    {
        bool AddUser(UTable data);

        List<UTable> GetUsers();

        UTable GetUserById(int id);

        SaveProfResp UpdateUser(UTable data, HttpPostedFileBase image, TrainerData tdata);

        bool DeleteUserById(int id);
    }
}
