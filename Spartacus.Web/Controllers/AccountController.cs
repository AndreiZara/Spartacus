using Microsoft.Win32;
using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMain _sessionMain = BussinesLogic.GetMainBL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl = null)
        {
            if (returnUrl != null) ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin login, string returnUrl = null)
        {

            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Username = login.Username,
                    Password = login.Password,
                    Ip = Request.UserHostAddress,
                    LoginTime = DateTime.Now
                };

                var userLogin = _session.UserLogin(data); // RESULT FROM THE Business Logic

                if (userLogin)
                {
                    HttpCookie cookie = _session.GetCookie(login.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["Username"] = login.Username;

                    if (returnUrl != null)
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    TempData["ErrorMessage"] = "You have entered an invalid username or password";
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

        public ActionResult Register()
        {
            return View();
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
                    TempData["RegMessage"] = "Registration failed, the current username is already taken";
            }
            return View();
        }

        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Forgot(string userEmail)
        {
            if (ModelState.IsValid)
            {
                var token = _sessionMain.CreateToken(userEmail);
                if (token == null) return View(); // show email sent message even if the user does not exists

                string subject = "Reset password";

                var resetUrl = Url.Action("ResetPassword", "Account", new { token }, Request.Url.Scheme);
                string body = _sessionMain.PopulateBody(userEmail, resetUrl);
                
                await _sessionMain.SendEmailAsync(userEmail, subject, body);
            }
            TempData["SuccessMessage"] = "A link has been sent to your email address.";
            return View();
            
        }


        public ActionResult ResetPassword(string token)
        {
            if (_sessionMain.IsResetTokenValid(token) || true)
            {
                TempData["ResetToken"] = token;
                return View();

            }
            TempData["ErrorMessage"] = "Link expired!";
            return RedirectToAction("Forgot", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword reset)
        {
            var token = TempData["ResetToken"] as string;
            if ( ModelState.IsValid)
            {
                var passReseted = _sessionMain.ResetPasswordByToken(token, reset.NewPassword);
                if (passReseted)
                {
                    TempData["SuccessMessage"] = "Password has been reset.";
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    TempData["ErrorMessage"] = "Reset password failed!";
                    return RedirectToAction("Forgot", "Account");
                }
            }

            TempData["ResetToken"] = token;
            return View(reset);
        }
    }

}