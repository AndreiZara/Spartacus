using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using System.Web;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface ISession
    {
        bool UserLogin(ULoginData data);
        bool UserReg(URegData data);
        HttpCookie GetCookie(string data);
        UserMinimal GetUserByCookie(string cookie);
        bool AddMembershipFor(string username, MsDuration? duration);
        bool SetMsDurationFor(string userCookie, MsDuration duration);
        MsDuration? GetMsDurationFor(string userCookie);
    }
}
