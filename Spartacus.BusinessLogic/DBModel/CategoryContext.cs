using System.Data.Entity;
using Spartacus.Domain.Entities.Membership;

namespace Spartacus.BusinessLogic.DBModel
{
    class CategoryContext : DbContext
    {
        public CategoryContext() : base("name=Spartacus")  {}

        public virtual DbSet<CatTable> Categories { get; set; }
    }
}
