using Spartacus.Domain.Entities.Membership;
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
        UProfData GetProfileByCookie(string cookie);
        SaveProfResp SaveProfileByCookie(string cookie, UProfData data);
        AddMemResp AddMembershipFor(string username, MsData data);
        string GetQrById(int id);
    }
}
