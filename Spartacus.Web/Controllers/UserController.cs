using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Allow(URole.Admin, URole.Manager)]
    public class UserController : BaseController
    {
        private readonly IAdmin _admin;

        public UserController()
        {
            _admin = new BussinesLogic().GetAdminBL();
        }
        public ActionResult Create()
        {
            SessionStatus();
            return View();
        }

        [HttpPost]
        public ActionResult Create(UTable data)
        {
            if (ModelState.IsValid)
            {
                data.LastLogin = DateTime.Now;
                data.LastIp = Request.UserHostAddress;
                _admin.AddUser(data);
                return RedirectToAction("Read");
            }
            return View(data);
        }

        public ActionResult Read()
        {
            SessionStatus();
            var users = _admin.GetUsers();
            return View(users);
        }

        public ActionResult Update(int id)
        {
            SessionStatus();
            var user = _admin.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Update(UTable data)
        {
            if (ModelState.IsValid)
            {
                data.LastLogin = DateTime.Now;
                data.LastIp = Request.UserHostAddress;

                var userUpdated = _admin.UpdateUser(data);

                if (userUpdated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("UpdateMessage", "Update failed!");
            }
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            SessionStatus();
            var user = _admin.GetUserById(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var userDeleted = _admin.DeleteUserById(id);
            if (userDeleted == false) return HttpNotFound();
            return RedirectToAction("Read");
        }
    }
}