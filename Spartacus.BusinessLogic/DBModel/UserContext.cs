using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : base("name=Spartacus") { }
        public virtual DbSet<UTable> Users { get; set; }
        public virtual DbSet<MsTable> Memberships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UTable>()
                        .HasOptional(e => e.Membership)
                        .WithRequired(e => e.User)
                        .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
