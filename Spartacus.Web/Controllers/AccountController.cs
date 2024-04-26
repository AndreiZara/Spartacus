using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;

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

        public ActionResult Register()
        {
            ViewBag.Roles = new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Administrator" },
                new SelectListItem { Value = "User", Text = "Regular User" }
            };
            return View();
        }

        [HttpPost]
        public ActionResult Register(UTable login)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
        }


        public ActionResult Login()
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


                //var userLogin = false; // RESULT FROM THE Business Logic

                var userLogin = _session.UserLogin(data);

                if (userLogin.Status)
                {
                    Session["Username"] = login.Username;
                    HttpCookie cookie = _session.GenCookie(login.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", userLogin.StatusMsg);
                    return View();
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
            if(Session != null)
            {
                Session["Username"] = null;
                System.Web.HttpContext.Current.Session.Clear();
                if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
                {
                    var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    }
                }
            }
            
            return RedirectToAction("Index", "Home");
            
            
        }


        public ActionResult Profile(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Credential = login.Username,
                    Password = login.Password,
                    Ip = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                var userTable = _session.UserProfile(data);

                
            }
            return View();
        }

        
    }
}