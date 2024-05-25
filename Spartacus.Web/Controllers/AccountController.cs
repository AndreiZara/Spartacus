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

        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Forgot(ForgotPassword pass)
        {
            if (ModelState.IsValid)
            {
                var token = _sessionMain.CreateToken(pass.Email);
                if (token == null) return View(); // show email sent message even if the user does not exists

                string subject = "Reset password";
                
                var resetUrl = Url.Action("ResetPassword", "Account", new {token = token}, Request.Url.Scheme);
                string body = _sessionMain.PopulateBody("helloworld", resetUrl, "helloworld");
                
                await _sessionMain.SendEmailAsync(pass.Email, subject, body);
            }
            return View();
        }


        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            //Session["token"] = token;
            //UToken guid = _sessionMain.GetToken(token);
            //DateTime now = DateTime.Now;
            //if(now < guid.EndDate) 
            //{
            //    UTable uTable = _sessionAdmin.GetUserByEmail(guid.Email);
            //    return RedirectToAction("Update", "User", new {id = uTable.Id});
            //}

            return View();
        }

        public new ActionResult Profile()
        {
            // should load a model containing mstable and utable data
            return View();
        }
    }

}