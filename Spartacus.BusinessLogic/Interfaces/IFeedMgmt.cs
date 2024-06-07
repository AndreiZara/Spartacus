using Spartacus.Domain.Entities.User;
using System.Collections.Generic;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IFeedMgmt
    {
        List<FBTable> GetFeeds();

        FBTable GetFeedById(int id);

        bool DeleteFeedById(int id);
    }
}
