using System.Web;
using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;

namespace Spartacus.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public ULoginResp UserLogin(ULoginData data) => UserLoginAction(data);
        public ULoginResp UserProfile(ULoginData data) => UserLoginAction(data);
        public HttpCookie GenCookie(string loginCredential) => Cookie(loginCredential);
        public UserMinimal GetUserByCookie(string apiCookieValue) => UserCookie(apiCookieValue);
    }
}
