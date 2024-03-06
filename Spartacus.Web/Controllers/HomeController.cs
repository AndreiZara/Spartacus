using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Membership()
        {
            return View();
        }
        public ActionResult Trainer() 
        { 
            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
    }
}