using Spartacus.Domain.Entities.User;
using System.Collections.Generic;

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
