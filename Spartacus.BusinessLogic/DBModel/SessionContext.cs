using Spartacus.Domain.Entities.User;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=CCToolShop")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
