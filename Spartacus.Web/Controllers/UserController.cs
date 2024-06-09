using AutoMapper;
using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Helpers;
using Spartacus.Web.Filters;
using Spartacus.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    [Allow(URole.Admin, URole.Manager)]
    public class UserController : BaseController
    {
        private readonly IUserMgmt _userMgmt = BussinesLogic.GetUserMgmtBL();
        private readonly ICatMgmt _catMgmt = BussinesLogic.GetCatMgmtBL();
        private readonly ILocMgmt _locMgmt = BussinesLogic.GetLocMgmtBL();
        private readonly IMain _main = BussinesLogic.GetMainBL();

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
        public async Task<ActionResult> Create(UTable data)
        {
            if (ModelState.IsValid)
            {
                data.LastLogin = DateTime.Now;
                data.LastIp = Request.UserHostAddress;
                data.Password = LoginHelpers.HashGen(data.Password);
                var userCreated = _userMgmt.AddUser(data);

                var token = _main.CreateToken<RegisterToken>(data.Email, 1440);
                if (userCreated && token != null)
                {
                    TempData["SuccessMessage"] = "Account needs to be activated through link sent to your email.";
                    string subject = "Registration on Spartacus";
                    var resetUrl = Url.Action("ConfirmRegister", "Account", new { token }, Request.Url.Scheme);
                    string body = _main.PopulateBody(data.Email, resetUrl, "~/Content/Template/RegisterEmail.html");

                    await _main.SendEmailAsync(data.Email, subject, body);
                
                    return RedirectToAction("Read");
                }
                else
                    TempData["ErrorMessage"] = "Creation failed.";
            }
            return View(data);
        }

        public ActionResult Update(int id)
        {
            SessionStatus();
            var user = _userMgmt.GetUserById(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UTable, UserUpdate>());
            var userUpdate = config.CreateMapper().Map<UserUpdate>(user);
            userUpdate.CatId = user.Membership?.CatId;
            userUpdate.LocId = user.Membership?.LocId;
            userUpdate.Period = user.Membership?.Period;

            userUpdate.Categories = new SelectList(_catMgmt.GetCats(), "Id", "Title");
            userUpdate.Locations = new SelectList(_locMgmt.GetLocs(), "Id", "Name");

            userUpdate.Activity = user.Trainer?.Activity;
            userUpdate.Bio = user.Trainer?.Bio;

            return View(userUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UserUpdate data, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserUpdate, UProfData>());
                var user = config.CreateMapper().Map<UProfData>(data);

                var userUpdated = _userMgmt.UpdateUser(user, Image);

                TempData["ErrorMessage"] = userUpdated switch
                {
                    SaveProfResp.Failed => "Changes failed to save.",
                    SaveProfResp.FailedUsername => "Username is taken.",
                    SaveProfResp.FailedImage => "Your image could not be saved.",
                    SaveProfResp.Success => null,
                    _ => throw new InvalidOperationException()
                };


                AddMemResp memUpdated = AddMemResp.Success;
                if (userUpdated == SaveProfResp.Success && data.SetMembership)
                {
                    memUpdated = _session.AddMembershipFor(data.Username, new MsData
                    {
                        CatId = data.CatId,
                        Period = data.Period,
                        LocId = data.LocId
                    });

                    TempData["ErrorMessage"] = memUpdated switch
                    {
                        AddMemResp.Failed => "Membership not saved.",
                        AddMemResp.FullCapacity => "Location is full.",
                        AddMemResp.Success => null,
                        _ => throw new InvalidOperationException()
                    };
                }

                if (userUpdated == SaveProfResp.Success && memUpdated == AddMemResp.Success)
                {
                    TempData["SuccessMessage"] = "User updated.";
                    return RedirectToAction("Update");
                }
            }
            data.Categories = new SelectList(_catMgmt.GetCats(), "Id", "Title");
            data.Locations = new SelectList(_locMgmt.GetLocs(), "Id", "Name");

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
        [ValidateAntiForgeryToken]
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

        public ActionResult RemoveUnconfirmed()
        {
            _userMgmt.RemoveUnconfirmedUsers();
            return RedirectToAction("Read", "User");
        }
    }
}