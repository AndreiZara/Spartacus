using Spartacus.Domain.Entities.User;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBContext
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=Spartacus") { }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
