using Spartacus.Web.Models;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Spartacus.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index(UserLogin login)
        {
            login.Username = "admin";
            return View(login);
        }
        
        public ActionResult Contact()
        {
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