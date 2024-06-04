using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.Interfaces
{
    public interface IFeedMgmt
    {
        List<FBTable> GetFeeds();

        FBTable GetFeedById(int id);

        bool DeleteFeedById(int id);
    }
}
