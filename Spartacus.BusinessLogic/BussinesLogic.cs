using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic.Logics;

namespace Spartacus.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL() => new SessionBL();
        public IAdmin GetAdminBL() => new AdminBL();
    }
}
