using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class AccountController : BaseController
    {
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

                var userLogin = _session.UserLogin(data); // RESULT FROM THE Business Logic

                if (userLogin)
                {
                    HttpCookie cookie = _session.GetCookie(login.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["Username"] = login.Username;

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("LoginMessage", "You have entered an invalid username or password");
            }
            return View();
        }

        public ActionResult Logout()
        {
            // https://stackoverflow.com/questions/54518454/how-to-create-logoff-in-c-sharp-with-asp-net-mvc-and-entity-framework
            EatCookie();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Register(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                URegData data = new URegData
                {
                    Username = register.Username,
                    Firstname = register.Firstname,
                    Lastname = register.Lastname,
                    Email = register.Email,
                    Password = register.Password,
                    Ip = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                var userReg = _session.UserReg(data);

                if (userReg)
                {
                    HttpCookie cookie = _session.GetCookie(register.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["Username"] = register.Username;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("RegMessage", "Registration failed, the current username is already taken");
                }
            }

            return View();
        }

        public new ActionResult Profile()
        {
            // should load a model containing mstable and utable data
            return View();
        }
    }
}