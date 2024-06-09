using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using System;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICatMgmt _mgmt = BussinesLogic.GetCatMgmtBL();
        private readonly IMain _main = BussinesLogic.GetMainBL();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(FeedData data)
        {
            if (ModelState.IsValid)
            {
                data.DateSent = DateTime.Now;
                _main.SendFeedback(data);
                TempData["SuccessMessage"] = "Thanks for your feedback.";
            }
            return RedirectToAction("Contact");
        }

        public ActionResult About()
        {
            SessionStatus();
            return View();
        }

        public ActionResult Membership()
        {
            SessionStatus();
            var data = _mgmt.GetCats();
            return View(data);
        }

        public ActionResult Trainers()
        {
            SessionStatus();
            var data = _main.GetTrainers();
            return View(data);
        }

        public ActionResult Services()
        {
            SessionStatus();
            return View();
        }
    }
}