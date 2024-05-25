using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class AccountController : BaseController
    {
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegister register)
        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Forgot(FrogotPassword pass)
        {
            if (ModelState.IsValid)
            {
                URegData data = new URegData
                if (_sessionAdmin.GetUserByEmail(pass.Email) == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                Session["Emali"] = pass.Email;

                string token = Guid.NewGuid().ToString();
                UToken guid = new UToken()
                {
                    Username = register.Username,
                    Firstname = register.Firstname,
                    Lastname = register.Lastname,
                    Email = register.Email,
                    Password = register.Password,
                    Ip = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                    Token = token,
                    EndDate = DateTime.Now.AddMinutes(5),
                    Email = pass.Email
                };

                var userReg = _session.UserReg(data);


                _sessionMain.CreateToken(guid);

                Session["token"] = token;


                string subject = "helloworld";


                string resetUrl = "http://localhost:51229/Account/ResetPassword?token=" + token;

                string body = _sessionMain.PopulateBody("helloworld", resetUrl, "helloworld");

                await _sessionMain.SendEmailAsync(pass.Email, subject, body);
            }

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
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            Session["token"] = token;
            UToken guid = _sessionMain.GetToken(token);
            DateTime now = DateTime.Now;
            if(now < guid.EndDate) 
            {
                UTable uTable = _sessionAdmin.GetUserByEmail(guid.Email);
                return RedirectToAction("Update", "User", new {id = uTable.Id});
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