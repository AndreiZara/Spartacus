﻿using Microsoft.Win32;
using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Spartacus.Web.Controllers
{

    public class AccountController : Controller
    {
        private readonly ISession _session;

        public AccountController()
        {
            var bl = new BussinesLogic();
            _session = bl.GetSessionBL();
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

                var userLogin = _session.UserLogin(data); // RESULT FROM THE Business Logic

                if (userLogin)
                {
                    Session["Username"] = login.Username;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginMessage", "You have entered an invalid username or password");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            // https://stackoverflow.com/questions/54518454/how-to-create-logoff-in-c-sharp-with-asp-net-mvc-and-entity-framework
            Session["Username"] = null;
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
            if(ModelState.IsValid)
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


        public ActionResult Details()
        {
            var users = new AdminApi().GetUsersAction();
            var user = users.FirstOrDefault(x => x.Username == Session["Username"] as string);
            if (user == null) return HttpNotFound();
            return View(user);
        }
    }
}