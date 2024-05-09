using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Spartacus.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ISession _session = BussinesLogic.GetSessionBL();

        protected bool SessionStatus()
        {
            var cookie = Request.Cookies["UserCookie"];
            if (cookie != null)
            {
                var user = _session.GetUserByCookie(cookie.Value);
                if (user != null)
                {
                    System.Web.HttpContext.Current.Session["LoginStatus"] = "login";
                    return true;
                }
                else
                {
                    Session.Abandon();
                    EatCookie();
                    return false;
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["LoginStatus"] = "logout";
                return false;
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