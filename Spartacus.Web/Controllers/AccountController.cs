using AutoMapper;
using QRCoder;
using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.Tokens;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using Spartacus.Helpers;
using Spartacus.Web.Models;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMain _main = BussinesLogic.GetMainBL();
        private readonly ICatMgmt _catMgmt = BussinesLogic.GetCatMgmtBL();

        public ActionResult Index()
        {
            SessionStatus();
            var prof = _session.GetProfileByCookie(Request.Cookies["UserCookie"].Value);
            if (prof == null)
            {
                TempData["ErrorMessage"] = "Please log in.";
                return RedirectToAction("Login", new { returnUrl = Request.Url.PathAndQuery });
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<UProfData, UserProfile>());
            var profile = config.CreateMapper().Map<UserProfile>(prof);
            
            if(prof.CatId != null)
            {
                var cat = _catMgmt.GetCatById(prof.CatId.Value);
                profile.Title = cat?.Title;
                profile.Description = cat?.Description;
            }

            if (profile.EndTime != null)
                profile.RemainingDays = (profile.EndTime - DateTime.Now).Value.Days;

            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserProfile profile, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<UserProfile, UProfData>());
                var prof = config.CreateMapper().Map<UProfData>(profile);
                prof.Image = Image;

                prof.Password = LoginHelpers.HashGen(profile.Password);
                var profSaved = _session.SaveProfileByCookie(Request.Cookies["UserCookie"].Value, prof);

                TempData["ErrorMessage"] = profSaved switch
                {
                    SaveProfResp.Failed => "Changes failed to save.",
                    SaveProfResp.FailedUsername => "You can't change your username at the moment.",
                    SaveProfResp.FailedImage => "Your image could not be saved.",
                    SaveProfResp.Success => null,
                    _ => throw new InvalidOperationException()
                };
                TempData["SuccessMessage"] = (profSaved == SaveProfResp.Success) ? "Your changes has been saved." : null;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Login(string returnUrl = null)
        {
            SessionStatus();
            if (returnUrl != null) ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData
                {
                    Username = login.Username,
                    Password = LoginHelpers.HashGen(login.Password),
                    Ip = Request.UserHostAddress,
                    LoginTime = DateTime.Now
                };

                var userLogin = _session.UserLogin(data);

                if (userLogin)
                {
                    HttpCookie cookie = _session.GetCookie(login.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["Username"] = login.Username;

                    if (returnUrl != null)
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Account");
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
            SessionStatus();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                URegData data = new URegData
                {
                    Username = register.Username,
                    Firstname = register.Firstname,
                    Lastname = register.Lastname,
                    Email = register.Email,
                    Password = LoginHelpers.HashGen(register.Password),
                    Ip = Request.UserHostAddress,
                    LoginDateTime = DateTime.Now
                };

                var userReg = _session.UserReg(data);

                if (userReg)
                {
                    HttpCookie cookie = _session.GetCookie(register.Username);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    Session["Username"] = register.Username;

                    var token = _main.CreateToken<RegisterToken>(register.Email, 1440);
                    if (token == null)
                    {
                        TempData["ErrorMessage"] = "Invalid registration.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Your account needs to be activated through link sent to your email.";
                        string subject = "Registration on Spartacus";

                        var resetUrl = Url.Action("ConfirmRegister", "Account", new { token }, Request.Url.Scheme);
                        string body = _main.PopulateBody(register.Email, resetUrl, "~/Content/Template/RegisterEmail.html");

                        await _main.SendEmailAsync(register.Email, subject, body);
                    }

                    return RedirectToAction("Index", "Account");
                }
                else
                    TempData["RegMessage"] = "Registration failed, the current username or email is already taken";
            }
            return View(register);
        }

        public ActionResult ConfirmRegister(string token)
        {
            if (!_main.ConfirmRegisterToken(token))
            {
                TempData["ErrorMessage"] = "Link expired! Your account has been deleted.";
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Forgot(string userEmail)
        {
            if (ModelState.IsValid)
            {
                var token = _main.CreateToken<ResetToken>(userEmail, 60);
                if (token == null)
                {
                    TempData["SuccessMessage"] = "A link has been sent to your email address.";
                    return View(); // show email sent message even if the user does not exists
                }

                string subject = "Reset password";

                var resetUrl = Url.Action("ResetPassword", "Account", new { token }, Request.Url.Scheme);
                string body = _main.PopulateBody(userEmail, resetUrl, "~/Content/Template/ResetEmail.html");

                await _main.SendEmailAsync(userEmail, subject, body);
            }
            TempData["SuccessMessage"] = "A link has been sent to your email address.";
            return View();
        }

        public ActionResult ResetPassword(string token)
        {
            if (_main.IsResetTokenValid(token))
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
            if (ModelState.IsValid)
            {
                var passReseted = _main.ResetPasswordByToken(token, reset.NewPassword);
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

        public ActionResult GetQr()
        {
            SessionStatus();

            var current = _session.GetUserByCookie(Request.Cookies["UserCookie"].Value);
            string qrToken = _session.GetQrById(current.Id);

            var data = new QRCodeGenerator().CreateQrCode(qrToken, QRCodeGenerator.ECCLevel.Q);
            BitmapByteQRCode qr = new BitmapByteQRCode(data);
            return File(qr.GetGraphic(20), "image/png");
        }

        public ActionResult ShowQr()
        {
            SessionStatus();
            TempData["ShowQr"] = "Show";
            return RedirectToAction("Index");
        }
    }

}