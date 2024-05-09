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

        public bool AddMembershipFor(string username, MsDuration? duration) => AddMembershipForAction(username, duration);

        public bool SetMsDurationFor(string userCookie, MsDuration dur) => SetMsDurationForAction(userCookie, dur);

        public MsDuration? GetMsDurationFor(string userCookie) => GetMsDurationForAction(userCookie);
    }
}
