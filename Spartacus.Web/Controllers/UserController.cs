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
using Spartacus.Domain.Entities.Services;

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

        [AdminMod(Domain.Enums.URole.Admin)]
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

                _main.UploadFile(file);                
                _admin.AddUser(data);                
            }
            return View(login);
        }

        [AdminMod(Domain.Enums.URole.Admin, Domain.Enums.URole.Moderator)]
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


        [AdminMod(Domain.Enums.URole.Admin, Domain.Enums.URole.Moderator)]
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

        [AdminMod(Domain.Enums.URole.Admin)]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int Id)
        {
            var user = _admin.GetUserById(Id);
            if (user != null)
            {
                UFile file = new UFile
                {
                    FileModel = user.File,
                    Username = user.Username,
                };

                bool isTrue = _admin.DeleteUser(Id);
                bool itTrueFile = _main.DeleteFile(file);

                if (isTrue && itTrueFile)
                {
                    return RedirectToAction("Read");
                }
            }
            return View();
        }

        [AdminMod(Domain.Enums.URole.Admin)]
        public ActionResult CreateDetail()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDetail(MenDetTable table)
        {
            if (ModelState.IsValid)
            {
                UTable user = _admin.GetUserByUsername(table.Username);
                SerTable serTable = new SerTable
                {
                    ServiceId = 0,
                    Title = table.Activity,
                    Description = table.Description,
                    File = user.File,
                    FileName = user.FileName,
                    MenDetTables = new List<MenDetTable>()
                };

                serTable.MenDetTables.Add(table);

                MenDetTable data = new MenDetTable
                {
                    Username = table.Username,
                    Id = table.Id,
                    Description = table.Description,
                    Activity = table.Activity,
                    ServiceId = 0,
                    SerTable = serTable
                };

                _admin.AddDetail(data);


            }
            return View(table);
        }

        [AdminMod(Domain.Enums.URole.Admin, Domain.Enums.URole.Moderator)]
        public ActionResult UpdateDetail(MenDetTable table)
        {
            MenDetTable newTable = _admin.GetDetailById(table.Id);
            return View(newTable);
        }

        public ActionResult UpdateDetail(MenDetTable table, int Id)
        {

            MenDetTable newTable = _admin.GetDetailById(Id);

            if(newTable != null)
            {
                newTable.Id = Id;
                newTable.Description = table.Description;
                newTable.Activity = table.Activity;
                newTable.SerTable = new Domain.Entities.Services.SerTable();
            }

            bool isTrue = _admin.UpdateDetail(newTable, newTable.Id);

            if (isTrue)
            {
                return RedirectToAction("Read");
            }

            return RedirectToAction("Create", new {id = Id});
        }

        [HttpGet]
        [AdminMod(Domain.Enums.URole.Admin, Domain.Enums.URole.Moderator)]
        public ActionResult ReadDetail()
        {
            SessionStatus();

            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }

            List<MenDetTable> Dlist = new List<MenDetTable>();
            MenDetTable newTable = new MenDetTable();

            Dlist = _admin.ReadDetail();
            return View(Dlist);
        }


        [AdminMod(Domain.Enums.URole.Admin)]
        public ActionResult DeleteDetail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDetail(int Id)
        {
            bool isTrue = _admin.DeleteDetail(Id);
            if (isTrue) { return RedirectToAction("Read"); }
            return View();
        }


    }

}