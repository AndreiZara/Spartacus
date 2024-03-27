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

namespace Spartacus.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Create(UserLogin login)
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
            }

            var userLogin = false;  

            if (userLogin)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("DeBil", "DeBil");
                Session["Username"] = login.Username;
                return View(login);
            }

        }

        /*    [HttpPost]
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
            }

            var userLogin = false; // RESULT FROM THE Business Logic

            if (userLogin)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("DeBil", "DeBil");
                Session["Username"] = login.Username;
                return RedirectToAction("Index", "Home");
            }
        }*/

        
        public ActionResult MUSer(UserLogin login)
        {
            UserTable user = new UserTable();
            user.UserList = new List<UserLogin>();
            
            return View(user);
        }


        public ActionResult Delete()
        {


            return View();
        }

      
    }
}