using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ISession _session = new BussinesLogic().GetSessionBL();

        protected void SessionStatus()
        {
            var cookie = Request.Cookies["UserCookie"];
            if (cookie != null)
            {
                var user = _session.GetUserByCookie(cookie.Value);
                if (user != null)
                {
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                }
                else 
                { 
                    Session.Abandon();
                    EatCookie();
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
            }
        }

        protected void EatCookie()
        {
            System.Web.HttpContext.Current.Session.Clear();
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("UserCookie"))
            {
                var cookie = ControllerContext.HttpContext.Request.Cookies["UserCookie"];
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                }
            }

            System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
        }
    }
}