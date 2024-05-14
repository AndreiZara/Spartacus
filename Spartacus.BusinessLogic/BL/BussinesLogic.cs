using Spartacus.BusinessLogic.BL;
using Spartacus.BusinessLogic.Interfaces;

namespace Spartacus.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IAdmin GetAdminBL()
        {
            return new AdminBL();
        }

        public IMain GetMainBL()
        {
            return new MainBL();
        }
    }
}
