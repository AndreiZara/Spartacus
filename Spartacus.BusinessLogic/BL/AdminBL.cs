using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.Services;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.BL
{
    public class AdminBL:AdminApi,IAdmin
    {
        public void AddUser(UTable user) => AddUserAction(user);
       
        public bool UpdateUser(UTable user, int id)  => UpdateUserAction(user, id);
       
        public List<UTable> ReadUser() => ReadUserAction();

        public bool UpdateCategory(CategoryTable category, int Id) => UpdateCategoryAction(category, Id);

        public bool DeleteUser(int Id) => DeleteUserAction(Id);

        public UTable GetUserById(int Id) => GetUserByIdAction(Id);

        public UTable GetParticularUserById(int Id) => GetParticularUserByIdAction(Id);

        public UTable GetUserByEmail(string Email) => GetUserByEmailAction(Email);

        public UTable GetUserByUsername(string Username) => GetUserByUsernameAction(Username);

        public void AddCategory(CategoryTable table) => AddCategoryAction(table);

        public List<CategoryTable> ReadCategory() => ReadCategory();

        public CategoryTable GetCategoryById(int Id) => GetCategoryByIdAction(Id);

        public CategoryTable GetParticularCategoryById(int Id) => GetParticularCategoryByIdAction(Id);


        //Detail


        public void AddDetail(MenDetTable user) => AddDetailAction(user);

        public List<MenDetTable> ReadDetail() => ReadDetailAction();

        public bool UpdateDetail(MenDetTable user, int Id) => UpdateDetailAction(user, Id);

        public bool DeleteDetail(int Id) => DeleteDetailAction(Id);

        public MenDetTable GetDetailByUsername(string Username) => GetDetailByUsernameAction(Username);

        public MenDetTable GetDetailByActivity(string Activity) => GetDetailByActivity(Activity);

        public MenDetTable GetDetailById(int Id) => GetDetailByIdAction(Id);


        //Service


        public void AddService(SerTable user)=>AddServiceAction(user);

        public List<SerTable> ReadService() => ReadServiceAction();

        public bool UpdateService(SerTable user, int Id)=>UpdateServiceAction(user, Id);

        public bool DeleteService(int Id) => DeleteServiceAction(Id);

        public SerTable GetServiceByTitle(string Title) => GetServiceByTitleAction(Title);

        public SerTable GetServiceById(int Id) => GetServiceByIdAction(Id); 

        //Feedback

        public void AddFeedback(FBTable table) => AddFeedbackAction(table);

        public List<FBTable> ReadFeedback() => ReadFeedbackAction();

        public bool UpdateFeedback(FBTable table, int Id) => UpdateFeedbackAction(table, Id);

        public bool DeleteFeedback(int Id) => DeleteDetailAction(Id);

        public FBTable GetFeedbackById(int Id) => GetFeedbackByIdAction(Id);

        public FBTable GetFeedbackByEmail(string Email) => GetFeedbackByEmailAction(Email);
    }
}
