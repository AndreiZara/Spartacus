using Spartacus.Domain.Entities.User;
using System.Web;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface ISession
    {
        bool UserLogin(ULoginData data);
        bool UserReg(URegData data);
        HttpCookie GetCookie(string data);
        UProfData GetUserByCookie(string cookie);
        //UserMinimal GetUserByCookie(string apiCookieValue);
    }
}
