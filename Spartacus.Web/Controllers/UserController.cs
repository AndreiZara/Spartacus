using System.Web.Mvc;
using System;
using Spartacus.BusinessLogic.DBModel;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using System.Linq;
using Spartacus.BusinessLogic;

namespace Spartacus.Web.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IAdmin _admin;

        public UserController()
        {
            _admin = new BussinesLogic().GetAdminBL();
        }
        public ActionResult Create()
        {
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
            }
            return View(data);
        }
        public ActionResult Read()
        {
            var users = _admin.GetUsers();
            return View(users);
        }
        
        public ActionResult Update(int id)
        {
            var user = _admin.GetUserById(id);
            using (var db = new MembershipContext())
            {
                user.Membership = db.Memberships.FirstOrDefault(m => m.Id == user.MembershipId);
            }
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

                if(userUpdated)
                    return RedirectToAction("Read");
                else
                    ModelState.AddModelError("UpdateMessage", "Update failed!");
            }
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var user = _admin.GetUserById(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _admin.DeleteUserById(id);
            if (result == false) return HttpNotFound();
            return RedirectToAction("Read");
        }
    }
}