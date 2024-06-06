using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Allow(URole.Admin,  URole.Manager)]
    public class FeedController : BaseController
    {
        private readonly IFeedMgmt _mgmt = BussinesLogic.GetFeedMgmtBL();

        public ActionResult Read()
        {
            SessionStatus();
            var feeds = _mgmt.GetFeeds();
            return View(feeds);
        }

        public ActionResult Details(int id)
        {
            SessionStatus();
            var feed = _mgmt.GetFeedById(id);
            return View(feed);
        }

        public ActionResult Delete(int id)
        {
            SessionStatus();
            var feed = _mgmt.GetFeedById(id);
            if (feed == null) return HttpNotFound();
            return View(feed);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var feedDeleted = _mgmt.DeleteFeedById(id);
            if (feedDeleted == false) return HttpNotFound();
            return RedirectToAction("Read");
        }
    }
}