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

namespace Spartacus.Web.Controllers
{
    public class UserController : Controller
    {

        public ActionResult UCreate()
        {
            return View();
        }


        // GET: User
        [HttpPost]
        public ActionResult UCreate(UDbTable login)
        {

            if (ModelState.IsValid)
            {
                UDbTable data = new UDbTable
                {
                    Username = login.Username,
                    Id= login.Id,
                    Password = login.Password,
                    Firstname = login.Firstname,
                    Lastname = login.Lastname,
                    Email = login.Email,
                    LastLogin = DateTime.Now,
                    LastIp = "12345678",
                    Level = login.Level,
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

        
        public ActionResult Update(UDbTable login)
        {


            if (ModelState.IsValid)
            {
                UDbTable data = new UDbTable
                {
                    Username = login.Username,
                    Firstname = login.Firstname,
                    Lastname = login.Lastname,
                    Id = login.Id,
                    Password = login.Password,
                    Email = login.Email,
                    LastLogin = DateTime.Now,
                    LastIp = login.LastIp,
                    Level = login.Level,
                };
                
                bool isTrue = new AdminApi().UpdateUser(data,login.Id);

                if (isTrue)
                {
                    return RedirectToAction("Read");
                }

                else { return View(); }
            }
            Session["Id"] = login.Id;

            return View();
        }


        [HttpGet]
        public ActionResult URead() 
        {
            List<UDbTable> Ulist = new List<UDbTable>();
            UDbTable newTable = new UDbTable();
            AdminApi api = new AdminApi();
            Ulist = api.ReadUser();
            newTable = Ulist[3];

            return View(newTable);
        }
        
        [HttpGet]
        public ActionResult Read()
        {
            List<UDbTable> Ulist = new List<UDbTable>();
            UDbTable newTable = new UDbTable();
            AdminApi api = new AdminApi();
            Ulist = api.ReadUser();
            return View(Ulist);
        }



        

    }
}