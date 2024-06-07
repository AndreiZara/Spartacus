using Spartacus.BusinessLogic;
using Spartacus.BusinessLogic.Interfaces;
using System.Web;
using System.Web.Mvc;

namespace Spartacus.Web.Filters
{
    public class ConfirmedAttribute : ActionFilterAttribute
    {
        private readonly ISession _session = BussinesLogic.GetSessionBL();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookie = HttpContext.Current.Request.Cookies["UserCookie"];
            if (cookie != null)
            {
                var user = _session.GetUserByCookie(cookie.Value);
                if (!user?.IsConfirmed ?? false) filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}