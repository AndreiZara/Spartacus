using Spartacus.BusinessLogic.Interfaces;
using Spartacus.BusinessLogic.Logics;

namespace Spartacus.BusinessLogic
{
    public static class BussinesLogic
    {
        public static ISession GetSessionBL() => new SessionBL();
        public static IUserMgmt GetUserMgmtBL() => new UserMgmtBL();
        public static ICatMgmt GetCatMgmtBL() => new CatMgmtBL();
        public static IMain GetMainBL() => new MainBL();
        public static IFeedMgmt GetFeedMgmtBL() => new FeedMgmtBL();
    }
}
