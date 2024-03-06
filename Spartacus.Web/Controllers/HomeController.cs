using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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