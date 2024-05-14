using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.BL
{
    public class AdminBL:AdminApi,IAdmin
    {
        public void AddUser(UTable user)
        {
            AddUserAction(user);
        }

        public bool UpdateUser(UTable user, int id)
        {
            return UpdateUserAction(user, id);
        }

        public List<UTable> ReadUser()
        {
            return ReadUserAction();
        }

        
        public bool UpdateCategory(CategoryTable category, int Id)
        {
            return UpdateCategoryAction(category, Id);
        }

        public bool DeleteUser(int Id)
        {
            return DeleteUserAction(Id);
        }

        public UTable GetUserById(int Id)
        {
            return GetUserByIdAction(Id);
        }

        public UTable GetParticularUserById(int Id)
        {
            return GetParticularUserByIdAction(Id);
        }

        public UTable GetUserByUsername(string Username)
        {
            return GetUserByUsernameAction(Username);
        }

        public void AddCategory(CategoryTable table)
        {
            AddCategoryAction(table);
        }

        public List<CategoryTable> ReadCategory()
        {
            return ReadCategory();
        }

        public CategoryTable GetCategoryById(int Id)
        {
            return GetCategoryByIdAction(Id);
        }

        public CategoryTable GetParticularCategoryById(int Id)
        {
            return GetParticularCategoryByIdAction(Id);
        }

    }
}
