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
using Spartacus.Domain.Enums;
using Spartacus.Domain.Entities.Services;
using Spartacus.Domain.Entities.Tokens;

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

        public ActionResult Register(int level)
        {
            URole role = (URole)level;

            UTable user = new UTable()
            {
                Level = role
            };
            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Register(UTable login)
        {

            if (ModelState.IsValid)
            {
                string filename = login.File.FileName;
                UTable data = new UTable
                {
                    Username = login.Username,
                    Id = login.Id,
                    Password = login.Password,
                    Firstname = login.Firstname,
                    Lastname = login.Lastname,
                    Email = login.Email,
                    LastLogin = DateTime.Now,
                    LastIp = Request.UserHostAddress,
                    Level = login.Level,
                    File = login.File,
                    FileName = filename,
                };

                UFile file = new UFile()
                {
                    FileModel = login.File,
                    Username = login.Username,
                };


                _sessionMain.UploadFile(file);
                _sessionAdmin.AddUser(data);


                string token = Guid.NewGuid().ToString();

                RegisterToken guid = new RegisterToken()
                {
                    Token = token,
                    EndDate = DateTime.Now.AddMinutes(5),
                    Email = data.Email,
                    Status = Domain.Enums.TokenStatus.Default,
                };

                _sessionMain.CreateRegToken(guid);

                Session["token"] = token;


                string subject = "ConfirmRegistration";


                string resetUrl = "http://localhost:51229/Account/ConfirmRegist?token=" + token;

                string[] PageParameters = {"{Prologue}","{Url}" ,"{Title}", "{Description}"};
                string[] EmailParameters = {"Hello, you need to confirm you account", resetUrl, "HelloWorld", "HelloWord"};
                string PagePath = "~/Content/Template/Email.html";  

                string body = _sessionMain.PopulateBody(PagePath, PageParameters, EmailParameters);

                await _sessionMain.SendEmailAsync(data.Email, subject, body);

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
                        id = userTable.Id,
                        Username =  userTable.Username,
                        Password = userTable.Password,
                        Firstname = userTable.Firstname,
                        Lastname = userTable.Lastname,
                        Email = userTable.Email,
                        FilePath = "/Content/Upload/shrek/shrexy.jpg",
                        Level = userTable.Level,

                    };

                    if(_sessionAdmin.GetDetailByUsername(userTable.Username) == null)
                    {

                        MenDetTable trainer = new MenDetTable()
                        {
                            Username = userTable.Username,
                            Activity = "",
                            Description = ""
                        };

                        RoleDiv role = new RoleDiv
                        {
                            UModel = Model,
                            MenDet = trainer
                        };

                        Session["Path"] = FilePath;
                        Session["Level"] = userTable.Level.ToString();

                        return View(role);
                    }

                    MenDetTable mentor = _sessionAdmin.GetDetailByUsername(userTable.Username);

                    RoleDiv newRole = new RoleDiv
                    {
                        UModel = Model,
                        MenDet = mentor,
                    };

                    Session["Path"] = FilePath;
                    Session["Level"] = userTable.Level.ToString();
                    return View(newRole);
                }
            
            }
            return View();
            
        }

        [HttpPost]
        public ActionResult Profile(RoleDiv role)
        {
            UTable user = _sessionAdmin.GetUserByUsername(role.UModel.Username);

            UTable tmpUser = new UTable()
            {
                Id = user.Id,
                Username = role.UModel.Username,
                Firstname = role.UModel.Firstname,
                Lastname = role.UModel.Lastname,
                Email = role.UModel.Email,
                Password = role.UModel.Password,
                Level = user.Level,
                LastIp = user.LastIp,
                LastLogin = user.LastLogin,
                File = user.File,
            };

            role.UModel.Level = user.Level;
            _sessionAdmin.UpdateUser(tmpUser,tmpUser.Id);

            tmpModel newModel = new tmpModel
            {
                Username = tmpUser.Username,
                Firstname = tmpUser.Firstname,
                Lastname = tmpUser.Lastname,
                Email= tmpUser.Email,
                Password = tmpUser.Password,
                Level = tmpUser.Level,  
                File = tmpUser.File,
                FilePath = "/Content/Upload/shrek/shrexy.jpg"
            };


            if(_sessionAdmin.GetServiceByTitle(role.MenDet.Activity) != null)
            {
                SerTable service = _sessionAdmin.GetServiceByTitle(role.MenDet.Activity);
                MenDetTable mentor = new MenDetTable()
                {
                    Username = user.Username,
                    Description = role.MenDet.Description,
                    Activity = role.MenDet.Activity,
                    SerTable = service
                };


                MenDetTable menTable = _sessionAdmin.GetDetailByUsername(mentor.Username);

                if (menTable == null)
                {
                    _sessionAdmin.AddDetail(mentor);
                    return View();
                }

                _sessionAdmin.UpdateDetail(mentor, mentor.Id);

                role.UModel = newModel;
                role.MenDet = mentor;

                return View(role);
            }

            return View(role);


        }

        public ActionResult Choose()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Choose(string card)
        {
            if(card == "card1")
            {
                return RedirectToAction("Register", "Account", new {level = 1});
            }
            else if(card == "card2")
            {
                return RedirectToAction("Register", "Account", new {level = 4});
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
                ResetToken guid = new ResetToken()
                {
                    Token = token,
                    EndDate = DateTime.Now.AddMinutes(5),
                    Email = pass.Email
                };

                _sessionMain.CreateToken(guid);

                Session["token"] = token;


                string subject = "helloworld";


                string resetUrl = "http://localhost:51229/Account/RessetPassword?token=" + token;

                string[] PageParameters = { "{Prologue}", "{Url}", "{Title}", "{Description}" };
                string[] EmailParameters = { "Hello, here is your resset password link", resetUrl, "HelloWorld", "HelloWord" };
                string PagePath = "~/Content/Template/Email.html";

                string body = _sessionMain.PopulateBody(PagePath, PageParameters, EmailParameters);

                await _sessionMain.SendEmailAsync(pass.Email, subject, body);
            }
            
            return View();
        }

        [HttpGet]
        public ActionResult ResetPassword(string token)
        {
            Session["token"] = token;
            ResetToken guid = _sessionMain.GetToken(token);
            if(DateTime.Now < guid.EndDate) 
            {
                UTable uTable = _sessionAdmin.GetUserByEmail(guid.Email);
                return RedirectToAction("Update", "User", new {id = uTable.Id});
            }

            return View();
        }

        [HttpGet]
        public ActionResult ConfirmRegist(string token)
        {
            Session["token"] = token;
            RegisterToken guid = _sessionMain.GetRegToken(token);
            if(guid == null) { RedirectToAction("Register", "Account"); }
            if (DateTime.Now < guid.EndDate)
            {
                RegisterToken newToken = new RegisterToken
                {
                    Email = guid.Email,
                    Token = guid.Token,
                    EndDate = DateTime.Now.AddYears(100),
                    Status = Domain.Enums.TokenStatus.Confirmed,
                };
                _sessionMain.UpdateRegToken(newToken,guid.Id);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }

}