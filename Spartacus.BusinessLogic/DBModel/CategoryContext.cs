using Spartacus.Domain.Entities.Membership;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    class CategoryContext : DbContext
    {
        public CategoryContext() : base("name=Spartacus") { }

        public virtual DbSet<CatTable> Categories { get; set; }
    }
}
