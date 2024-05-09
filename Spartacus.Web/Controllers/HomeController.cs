using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMain _main;

        public HomeController()
        {
            _main = BussinesLogic.GetMainBL();
        }

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
            var data = _main.GetCats();
            return View(data);
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