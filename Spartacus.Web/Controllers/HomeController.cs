using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic;
using Spartacus.Domain.Entities.Feedback;
using Spartacus.Web.Models;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.ActionFilters;

namespace Spartacus.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMain _main;
        private readonly IAdmin _admin;

        public HomeController()
        {
            var bl = new BussinesLogic();
            _admin = bl.GetAdminBL();
            var main = new BussinesLogic();
            _main = main.GetMainBL();
        }

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Index(UserLogin login)
        //{
        //    login.Username = "admin";
        //    return View(login);
        //}

        public ActionResult Contact()
        {
            return View();
        }

        [AdminMod(Domain.Enums.URole.Admin, Domain.Enums.URole.Moderator, Domain.Enums.URole.Trainer, Domain.Enums.URole.Consumer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(FBTable table)
        {
            if (ModelState.IsValid)
            {
                
                var userList = _admin.ReadUser();
                UTable userTable = new UTable();
                foreach (var user in userList)
                {
                    if(user.Level == Domain.Enums.URole.Admin)
                    {
                        userTable = user;
                        break;
                    }
                    else if (user.Level == Domain.Enums.URole.Moderator)
                    {
                        userTable = user;
                        break;
                    }
                }

                FBTable newTabel = new FBTable
                {
                    Id = table.Id,
                    Username = table.Username,
                    Email = table.Email,
                    Subject = table.Subject,
                    Message = table.Message,
                    AdminUsername = userTable.Username,
                };

                _admin.AddFeedback(newTabel);
                

                string body = _main.PopulateBodyFeedback(newTabel);

                await _main.SendEmailAsync(userTable.Email, "helloWorld" , body);

                return View(newTabel);
            }
            return View();
        }


        public ActionResult About()
        {
            return View();
        }

        public ActionResult Membership()
        {
            return View();
        }

        public ActionResult Trainers()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }
    }
}