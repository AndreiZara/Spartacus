using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Location;
using Spartacus.Web.Filters;
using System;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Allow(Domain.Enums.URole.Admin)]
    public class LocationController : BaseController
    {
        private readonly ILocMgmt _mgmt = BussinesLogic.GetLocMgmtBL();
        
        public ActionResult Read()
        {
            SessionStatus();
            var locs = _mgmt.GetLocs();
            return View(locs);
        }

        public ActionResult Create()
        {
            SessionStatus();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LTable data)
        {
            if (ModelState.IsValid)
            {
                data.LastUpdate = DateTime.Now;
                var locCreated = _mgmt.AddLoc(data);

                if (locCreated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("CreateMessage", "Creation failed!");
            }
            return View();
        }

        public ActionResult Update(int id)
        {
            SessionStatus();
            var loc = _mgmt.GetLocById(id);
            return View(loc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(LTable data)
        {
            if (ModelState.IsValid)
            {
                data.LastUpdate = DateTime.Now;
                var locUpdated = _mgmt.UpdateLoc(data);

                if (locUpdated)
                    return RedirectToAction("Read");
                else
                    TempData["ErrorMessage"] = "Update failed!";
            }
            return View(data);
        }
        
        public ActionResult Delete(int id)
        {
            SessionStatus();
            var loc = _mgmt.GetLocById(id);
            if (loc == null) return HttpNotFound();
            return View(loc);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var locDeleted = _mgmt.DeleteLocById(id);
            if (locDeleted == false) return HttpNotFound();
            return RedirectToAction("Read");
        }
    }
}