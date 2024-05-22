using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Spartacus.BusinessLogic.Core;
using Spartacus.Web.ActionFilters;
using eUseControl.Web.Controllers;
using Spartacus.BusinessLogic.Interfaces;
using Microsoft.Ajax.Utilities;
using Spartacus.Domain.Entities.Membership;
using Spartacus.BusinessLogic;
using Spartacus.Web.Models;

namespace Spartacus.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMain _main;
        private readonly IAdmin _admin;

        public UserController()
        {
            var bl = new BussinesLogic();
            _admin = bl.GetAdminBL();
            var main = new BussinesLogic();
            _main = main.GetMainBL();
        }


        public ActionResult Create()
        {

            return View();
        }


        // GET: User
        [HttpPost]
        public ActionResult Create(UTable login)
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
                    LastIp = "12345678",
                    Level = login.Level,
                    File = login.File,
                    FileName = filename,
                };

                UFile file = new UFile()
                {
                    FileModel = login.File,
                    Username = login.Username,
                };

                _main.UploadFile(file);

                Session["Id"] = data.Id;
                Session["Username"] = data.Username;
                Session["Password"] = data.Password;
                Session["Email"] = data.Email;
                Session["LastLogin"] = data.LastLogin;
                Session["LastIp"] = data.LastIp;
                
                
                _admin.AddUser(data);
                
                
            }
            return View(login);

        }


        public ActionResult Update(UTable table)
        {
            var category = _admin.GetUserById(table.Id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Update(int id, UTable login)
        {
            string filename = login.File.FileName;
            var user = _admin.GetParticularUserById(id);

            if (user != null)
            {
                user.Username = login.Username;
                user.Firstname = login.Firstname;
                user.Lastname = login.Lastname;
                user.Id = login.Id;
                user.Password = login.Password;
                user.Email = login.Email;
                user.LastLogin = DateTime.Now;
                user.LastIp = Request.UserHostAddress;
                user.Level = login.Level;
                user.File = login.File;
                user.FileName = filename;

                UFile file = new UFile()
                {
                    FileModel = login.File,
                    Username = login.Username
                };

                _main.UploadFile(file);

                bool isTrue = _admin.UpdateUser(user,login.Id);
                if (isTrue) { return RedirectToAction("Read"); }
                return View(login);
            }

            return View();
        }


        [AdminMod]  
        [HttpGet]
        public ActionResult Read()
        {
            SessionStatus();
            
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            List<UTable> Ulist = new List<UTable>();
            UTable newTable = new UTable();
            
            Ulist = _admin.ReadUser();
            return View(Ulist);
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            bool isTrue = _admin.DeleteUser(Id);
            if(isTrue) { return RedirectToAction("Read"); }
            return View();
        }


        

    }
}