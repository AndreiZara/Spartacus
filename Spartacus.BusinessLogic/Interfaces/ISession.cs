using Spartacus.Domain.Entities.User;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface ISession
    {
        bool UserLogin(ULoginData data);
        bool UserReg(URegData data);

        //ULoginResp UserLogin(ULoginData data);
        //HttpCookie GenCookie(string loginCredential);
        //UserMinimal GetUserByCookie(string apiCookieValue);
    }
}
