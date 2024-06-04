using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using Spartacus.Domain.Entities.Membership;

namespace Spartacus.BusinessLogic.DBModel
{
    class CategoryContext : DbContext   
    {
        public CategoryContext() : base("name=Spartacus")  {}

        public virtual DbSet<CategoryTable> Categories { get; set; }

    }
}
