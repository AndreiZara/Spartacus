using AutoMapper;
using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using Spartacus.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Allow(URole.Admin, URole.Manager)]
    public class UserController : BaseController
    {
        private readonly IUserMgmt _userMgmt = BussinesLogic.GetUserMgmtBL();
        private readonly ICatMgmt _catMgmt = BussinesLogic.GetCatMgmtBL();

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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UserUpdate>());
            var userUpdate = config.CreateMapper().Map<UserUpdate>(user);

            userUpdate.Categories = new SelectList(_catMgmt.GetCats(), "Id", "Title");

            return View(userUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserUpdate data, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserUpdate, UTable>());
                var user = config.CreateMapper().Map<UTable>(data);
                user.LastLogin = DateTime.Now;
                user.LastIp = Request.UserHostAddress;

                var userUpdated = _userMgmt.UpdateUser(user, Image);

                TempData["ErrorMessage"] = userUpdated switch
                {
                    SaveProfResp.Failed => "Changes failed to save.",
                    SaveProfResp.FailedUsername => "Username is taken.",
                    SaveProfResp.FailedImage => "Your image could not be saved.",
                    SaveProfResp.Success => null,
                    _ => throw new InvalidOperationException()
                };


                if (userUpdated == SaveProfResp.Success && data.SetMembership)
                {
                    var memUpdated = _session.AddMembershipFor(data.Username, data.CatId, data.Period);
                    if (!memUpdated)
                    {
                        TempData["ErrorMessage"] = "Membership not saved.";
                        return RedirectToAction("Update");
                    }
                }

                if (userUpdated == SaveProfResp.Success)
                {
                    TempData["SuccessMessage"] = "User updated.";
                    return RedirectToAction("Update");
                }
            }
            return RedirectToAction("Update");
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

        public ActionResult Details(int id)
        {
            SessionStatus();
            var user = _userMgmt.GetUserById(id);
            return View(user);
        }
    }
}