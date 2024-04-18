using Spartacus.BusinessLogic.Core;
using Spartacus.Domain.Entities.User;
using Spartacus.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Profile(UDbTable login)
        {
            var user = new UDbTable()
            {
                Id = login.Id,
                Username = login.Username,
                Password = login.Password,
                Firstname = login.Firstname,
                Lastname = login.Lastname,
                Email = login.Email,
                LastIp = login.LastIp,
                LastLogin = login.LastLogin,
            };

            var tmp_user = new AdminApi().GetUserByUsername(user.Username);

            Session["Username"] = tmp_user.Username;
            Session["Firstname"] = tmp_user.Firstname;
            Session["Lastname"] = tmp_user.Lastname;
            Session["Email"] = tmp_user.Email;
            //var NewUser = new tmpModel()
            //{
            //    Id = login.Id,
            //    Username = user.Username,
            //    Firstname = user.Firstname,
            //    Lastname = user.Lastname,   
            //    Email = user.Email,
            //};

            return View(user); 

        }
    }
}