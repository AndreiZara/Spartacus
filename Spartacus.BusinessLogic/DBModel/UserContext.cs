using Spartacus.Domain.Entities.User;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() : base("name=Spartacus") // connectionstring name define in your web.config
        {

        }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
