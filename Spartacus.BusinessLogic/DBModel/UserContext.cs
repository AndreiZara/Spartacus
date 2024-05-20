using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using Spartacus.Domain.Entities.User;

namespace Spartacus.BusinessLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() : 
            base("name=Spartacus") // connectionstring name define in your web.config
        {
        }

        public virtual DbSet<UTable> Users{ get; set; }
        public virtual DbSet<UToken> Tokens { get; set; }

    }
}
