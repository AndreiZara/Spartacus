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
    }

    public class AboutController : Controller
    {
        // GET: Home
        public ActionResult About()
        {
            return View();
        }
    }
}