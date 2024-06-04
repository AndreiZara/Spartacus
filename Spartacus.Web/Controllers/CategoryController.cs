using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Allow(URole.Admin)]
    public class CategoryController : BaseController
    {
        private readonly ICatMgmt _catMgmt = BussinesLogic.GetCatMgmtBL();
        public ActionResult Read()
        {
            SessionStatus();
            var cats = _catMgmt.GetCats();
            return View(cats);
        }

        public ActionResult Create()
        {
            SessionStatus();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CatTable data)
        {
            if (ModelState.IsValid)
            {
                var catCreated = _catMgmt.AddCat(data);

                if (catCreated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("CreateMessage", "Creation failed!");
            }
            return View();
        }


        public ActionResult Update(int id)
        {
            SessionStatus();
            var cat = _catMgmt.GetCatById(id);
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CatTable data)
        {
            if (ModelState.IsValid)
            {
                var catUpdated = _catMgmt.UpdateCat(data);

                if (catUpdated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("UpdateMessage", "Update failed!");
            }
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            SessionStatus();
            var cat = _catMgmt.GetCatById(id);
            if (cat == null) return HttpNotFound();
            return View(cat);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var catDeleted = _catMgmt.DeleteCatById(id);
            if (catDeleted == false) return HttpNotFound();
            return RedirectToAction("Read");
        }
    }
}