using Spartacus.Domain.Entities.Membership;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    public class MembershipContext : DbContext
    {
        public MembershipContext() : base("name=Spartacus") { }
        public virtual DbSet<MsTable> Memberships { get; set; }
    }
}
