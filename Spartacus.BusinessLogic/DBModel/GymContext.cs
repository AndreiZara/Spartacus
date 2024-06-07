using Spartacus.Domain.Entities.Location;
using Spartacus.Domain.Entities.Membership;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    public class GymContext : DbContext
    {
        public GymContext() : base("name=Spartacus") { }
        
        public virtual DbSet<LTable> Locations { get; set; }
        public virtual DbSet<CatTable> Categories { get; set; }
    }
}
