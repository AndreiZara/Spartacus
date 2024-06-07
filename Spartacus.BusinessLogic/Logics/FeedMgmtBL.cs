using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Logics
{
    public class FeedMgmtBL : FeedMgmtApi, IFeedMgmt
    {
        public bool DeleteFeedById(int id) => DeleteFeedByIdAction(id);

        public FBTable GetFeedById(int id) => GetFeedByIdAction(id);

        public List<FBTable> GetFeeds() => GetFeedsAction();
    }
}
