using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using System.Web;

namespace Spartacus.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public bool UserLogin(ULoginData data) => UserLoginAction(data);

        public bool UserReg(URegData data) => UserRegAction(data);

        public HttpCookie GetCookie(string data) => GetCookieAction(data);

        public UserMinimal GetUserByCookie(string cookie) => GetUserByCookieAction(cookie);
    }
}
