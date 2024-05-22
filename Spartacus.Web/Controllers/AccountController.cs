using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Data;
using Microsoft.Ajax.Utilities;

namespace Spartacus.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly ISession _session;
        private readonly IMain _sessionMain;
        private readonly IAdmin _sessionAdmin;

        public AccountController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
            var Mbl = new BussinesLogic();
            _sessionMain = Mbl.GetMainBL();
            var Abl = new BussinesLogic();
            _sessionAdmin = Abl.GetAdminBL();
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
                UTable data = new UTable
                {
                    Username = login.Username,
                    Id = login.Id,
                    Password = login.Password,
                    Firstname = login.Firstname,
                    Lastname = login.Lastname,
                    Email = login.Email,
                    LastLogin = DateTime.Now,
                    LastIp = "12345678",
                    Level = login.Level
                };


                Session["Id"] = data.Id;
                Session["Username"] = data.Username;
                Session["Password"] = data.Password;
                Session["Email"] = data.Email;
                Session["LastLogin"] = data.LastLogin;
                Session["LastIp"] = data.LastIp;


                _sessionAdmin.AddUser(data);


            }
            
            return View(login);

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
                    ModelState.AddModelError("Username", "Please select an image to upload.");
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
            if (Session != null)
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


        public ActionResult Profile()
        {

            
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("X-KEY"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["X-KEY"];
                if (cookie != null)
                {
                    var user = _session.GetUserByCookie(cookie.Value);

                    UTable userTable = _sessionAdmin.GetUserByUsername(user.Username);

                    string FilePath = "/Content/Upload/" + userTable.Username + "/" + userTable.FileName;

                    //bool itTrue = _sessionMain.CheckFilePath(FilePath);
                    
                    tmpModel Model = new tmpModel()
                    {
                        Username = userTable.Username,
                        Password = userTable.Password,
                        Firstname = userTable.Firstname,
                        Lastname = userTable.Lastname,
                        FilePath = FilePath

                    };
                    Session["Path"] = FilePath;

                    return View(Model);
                }
            
            }
            return View();
            
        }


        public ActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Forgot(FrogotPassword pass)
        {
            if (ModelState.IsValid)
            {
                if (_sessionAdmin.GetUserByEmail(pass.Email) == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                Session["Emali"] = pass.Email;

                string token = Guid.NewGuid().ToString();
                UToken guid = new UToken()
                {
                    Token = token,
                    EndDate = DateTime.Now.AddMinutes(5),
                    Email = pass.Email
                };

                _sessionMain.CreateToken(guid);

                Session["token"] = token;


                string subject = "helloworld";


                string resetUrl = "http://localhost:51229/Account/ResetPassword?token=" + token;

                string body = _sessionMain.PopulateBody("helloworld", resetUrl, "helloworld");

                await _sessionMain.SendEmailAsync(pass.Email, subject, body);
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

        [HttpGet]
        public ActionResult Read()
        {
            List<UToken> Clist = new List<UToken>();
            Clist = _sessionMain.GetTokenList();
            return View(Clist);
        }

    }

}