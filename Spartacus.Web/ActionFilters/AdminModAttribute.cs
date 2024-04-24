using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Spartacus.Web.Extension;
using System.Web.Routing;

namespace Spartacus.Web.ActionFilters
{
    public class AdminModAttribute:ActionFilterAttribute
    {
        private readonly ISession _Session;
            
            public AdminModAttribute()
            {
                var bussinessLogic = new BussinesLogic();
                _Session = bussinessLogic.GetSessionBL();
            }

        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _Session.GetUserByCookie(apiCookie.Value);
                if (profile != null&&profile.Level != URole.Admin) 
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {controller = "Error", action = "Error404"}));
            }
        }

    }
}