using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;

namespace Spartacus.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public bool UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }
        public bool UserReg(URegData data)
        {
            return UserRegAction(data);
        }
    }
}
