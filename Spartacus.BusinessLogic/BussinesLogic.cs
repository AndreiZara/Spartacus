using Spartacus.BusinessLogic.Interfaces;

namespace Spartacus.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
