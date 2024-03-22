using Spartacus.Domain.Entities.User;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ULoginResp UserLogin(ULoginData data);
    }
}
