using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly ISession _session;

        public AccountController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
        }

        public ActionResult Login()
        {
            
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Name = login.Username,
                    Password = login.Password,
                    Ip = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };
            }

            var userLogin = false; // RESULT FROM THE Business Logic

            var user = new AdminApi().GetUserByUsername(login.Username);

            if(user != null) 
            {
                if(user.Username == login.Username&&user.Password == login.Password) {
                    Session["Username"] = login.Username;
                    return RedirectToAction("Profile", "UserProfile", new {Username = login.Username});
                }

                else
                {
                    ModelState.AddModelError("Incercati din nou", "DeBil");
                    return RedirectToAction("Login", "Account");
                }
            }

            return View(login);
         
        }

        public ActionResult Join()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["Username"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}