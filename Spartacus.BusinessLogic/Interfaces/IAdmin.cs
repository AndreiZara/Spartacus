using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        void AddUser(UTable data);

        List<UTable> GetUsers();

        UTable GetUserById(int id);

        bool UpdateUser(UTable data);

        bool DeleteUserById(int id);
    }
}
