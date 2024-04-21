using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.User;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() : base("name=Spartacus") { }
        public virtual DbSet<UTable> Users{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UTable>()
                        .HasOptional(u => u.Membership)
                        .WithOptionalPrincipal()
                        .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }
    }
}
