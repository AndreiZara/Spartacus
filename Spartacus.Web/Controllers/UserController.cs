using Spartacus.Domain.Entities.User;
using Spartacus.BusinessLogic;
using Spartacus.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Spartacus.BusinessLogic.Core;
using Spartacus.Domain.Enums;
using Spartacus.Web.ActionFilters;
using Spartacus.Web.Controllers;
using eUseControl.Web.Controllers;
using Spartacus.Web.Extension;

namespace Spartacus.Web.Controllers
{
    public class UserController : BaseController
    {

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
                
                AdminApi api = new AdminApi();
                api.AddUser(data);
                
                
            }
            return View(login);

        }

        
        public ActionResult Update(UTable login)
        {


            if (ModelState.IsValid)
            {
                UTable data = new UTable
                {
                    Username = login.Username,
                    Firstname = login.Firstname,
                    Lastname = login.Lastname,
                    Id = login.Id,
                    Password = login.Password,
                    Email = login.Email,
                    LastLogin = DateTime.Now,
                    LastIp = Request.UserHostAddress,
                    Level = login.Level,
                };
                
                bool isTrue = new AdminApi().UpdateUser(data,login.Id);

                if (isTrue)
                {
                    return View(data);
                }

                else { return View(data); }
            }
            Session["Id"] = login.Id;

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
            AdminApi api = new AdminApi();
            Ulist = api.ReadUser();
            return View(Ulist);
        }



        

    }
}