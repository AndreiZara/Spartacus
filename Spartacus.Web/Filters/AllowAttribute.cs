using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Enums;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Filters
{
    public class AllowAttribute : ActionFilterAttribute
    {
        private readonly ISession _session = BussinesLogic.GetSessionBL();
        private readonly URole[] _roles;

        public AllowAttribute(params URole[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookie = HttpContext.Current.Request.Cookies["UserCookie"];
            if (cookie != null)
            {
                var user = _session.GetUserByCookie(cookie.Value);
                if (user == null) filterContext.Result = new HttpNotFoundResult();
                else
                {
                    bool isAuthorized = false;
                    foreach (var role in _roles)
                    {
                        if (user.Role == role) isAuthorized = true;
                    }
                    if (!isAuthorized) filterContext.Result = new HttpNotFoundResult();
                }
            }
            else filterContext.Result = new HttpNotFoundResult();
        }
    }
}