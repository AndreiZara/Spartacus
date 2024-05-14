using Spartacus.Domain.Entities.Membership;
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
        void AddUser(UTable user);
        bool UpdateUser(UTable user, int id);
        List<UTable> ReadUser();
        bool UpdateCategory(CategoryTable category, int Id);
        void AddCategory(CategoryTable table);
        List<CategoryTable> ReadCategory();
        CategoryTable GetCategoryById(int Id);
        CategoryTable GetParticularCategoryById(int Id);
        UTable GetUserById(int Id);
        UTable GetUserByUsername(string Username);
        UTable GetParticularUserById(int Id);
        bool DeleteUser(int Id);
    }
}
