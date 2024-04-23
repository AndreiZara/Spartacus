using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            SessionStatus();
            return View();
        }

        public ActionResult Contact()
        {
            SessionStatus();
            return View();
        }

        public ActionResult About()
        {
            SessionStatus();
            return View();
        }

        public ActionResult Membership()
        {
            SessionStatus();
            return View();
        }

        public ActionResult Trainers()
        {
            SessionStatus();
            return View();
        }

        public ActionResult Services()
        {
            SessionStatus();
            return View();
        }
    }
}