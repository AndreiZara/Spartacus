using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class AccountController : Controller
    {
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
            if(ModelState.IsValid)
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

            if(userLogin)
            {
                return RedirectToAction("Index", "Home");
            } 
            else
            {
                ModelState.AddModelError("DeBil", "DeBil");
                Session["Username"] = login.Username;
                return RedirectToAction("Index", "Home");
            }
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