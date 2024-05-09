using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using System;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Allow(URole.Admin, URole.Manager)]
    public class UserController : BaseController
    {
        private readonly IUserMgmt _userMgmt = BussinesLogic.GetUserMgmtBL();

        public ActionResult Read()
        {
            SessionStatus();
            var users = _userMgmt.GetUsers();
            return View(users);
        }

        [Allow(URole.Admin)] 
        public ActionResult Create()
        {
            SessionStatus();
            return View();
        }

        [Allow(URole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UTable data)
        {
            if (ModelState.IsValid)
            {
                data.LastLogin = DateTime.Now;
                data.LastIp = Request.UserHostAddress;
                var userCreated = _userMgmt.AddUser(data);

                if (userCreated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("CreateMessage", "Creation failed!");
            }
            return View(data);
        }

        public ActionResult Update(int id)
        {
            SessionStatus();
            var user = _userMgmt.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UTable data)
        {
            if (ModelState.IsValid)
            {
                data.LastLogin = DateTime.Now;
                data.LastIp = Request.UserHostAddress;

                var userUpdated = _userMgmt.UpdateUser(data);

                if (userUpdated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("UpdateMessage", "Update failed!");
            }
            return View(data);
        }

        [Allow(URole.Admin)]
        public ActionResult Delete(int id)
        {
            SessionStatus();
            var user = _userMgmt.GetUserById(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [Allow(URole.Admin)]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var userDeleted = _userMgmt.DeleteUserById(id);
            if (userDeleted == false) return HttpNotFound();
            return RedirectToAction("Read");
        }
    }
}