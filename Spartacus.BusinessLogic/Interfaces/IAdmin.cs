using Spartacus.BusinessLogic.DBModel;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.Services;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
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
        UTable GetUserById(int Id);
        UTable GetUserByUsername(string Username);
        UTable GetParticularUserById(int Id);
        bool DeleteUser(int Id);
        UTable GetUserByEmail(string Email);
        //category tablle
        bool UpdateCategory(CategoryTable category, int Id);
        void AddCategory(CategoryTable table);
        List<CategoryTable> ReadCategory();
        CategoryTable GetCategoryById(int Id);
        CategoryTable GetParticularCategoryById(int Id);
        //details table
        void AddDetail(MenDetTable user);
        List<MenDetTable> ReadDetail();
        bool UpdateDetail(MenDetTable user, int Id);
        bool DeleteDetail(int Id);
        MenDetTable GetDetailByUsername(string Username);
        MenDetTable GetDetailByActivity(string Activity);
        MenDetTable GetDetailById(int Id);
        //Service table
        void AddService(SerTable user);
        List<SerTable> ReadService();
        bool UpdateService(SerTable user, int Id);
        bool DeleteService(int Id);
        SerTable GetServiceByTitle(string Title);
        SerTable GetServiceById(int Id);
        //Feedback
        void AddFeedback(FBTable table);
        List<FBTable> ReadFeedback();
        bool UpdateFeedback(FBTable table, int Id);
        bool DeleteFeedback(int Id);
        FBTable GetFeedbackById(int Id);
        FBTable GetFeedbackByEmail(string Email);
    }
}
