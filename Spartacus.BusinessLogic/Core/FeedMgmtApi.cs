using Spartacus.BusinessLogic.DBContext;
using Spartacus.Domain.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace Spartacus.BusinessLogic.Core
{
    public class FeedMgmtApi
    {
        internal List<FBTable> GetFeedsAction()
        {
            List<FBTable> feeds;
            using (var debil = new UserContext())
            {
                feeds = debil.Feedbacks.ToList();
            }
            return feeds;
        }

        internal FBTable GetFeedByIdAction(int id)
        {
            FBTable feed;
            using (var debil = new UserContext())
            {
                feed = debil.Feedbacks.FirstOrDefault(c => c.Id == id);
            }
            return feed;
        }

        internal bool DeleteFeedByIdAction(int id)
        {
            using (var debil = new UserContext())
            {
                var feed = debil.Feedbacks.Find(id);
                if (feed == null) return false;
                debil.Feedbacks.Remove(feed);
                debil.SaveChanges();
            }
            return true;
        }
    }
}
