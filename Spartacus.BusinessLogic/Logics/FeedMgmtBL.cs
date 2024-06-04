using Spartacus.BusinessLogic.Core;
using Spartacus.BusinessLogic.Interfaces;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Logics
{
    public class FeedMgmtBL : FeedMgmtApi, IFeedMgmt
    {
        public bool DeleteFeedById(int id) => DeleteFeedByIdAction(id);

        public FBTable GetFeedById(int id) => GetFeedByIdAction(id);

        public List<FBTable> GetFeeds() => GetFeedsAction();
    }
}
