using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using System.Web;

namespace Spartacus.BusinessLogic
{
    public class SessionBL : SessionApi, ISession
    {
        public bool UserLogin(ULoginData data) => UserLoginAction(data);

        public bool UserReg(URegData data) => UserRegAction(data);

        public HttpCookie GetCookie(string data) => GetCookieAction(data);

        public UserMinimal GetUserByCookie(string cookie) => GetUserByCookieAction(cookie);

        public UProfData GetProfileByCookie(string cookie) => GetProfileByCookieAction(cookie);

        public SaveProfResp SaveProfileByCookie(string cookie, UProfData data) => SaveProfileByCookieAction(cookie, data);

        public bool AddMembershipFor(string username, int? catId, MsDuration? duration) => AddMembershipForAction(username, catId, duration);

        public string GetQrById(int id) => GetQrByIdAction(id);
    }
}
