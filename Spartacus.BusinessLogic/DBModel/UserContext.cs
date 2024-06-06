using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using Spartacus.Domain.Entities.User;
using System.ComponentModel.DataAnnotations.Schema;
using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Entities.Services;
using Spartacus.Domain.Entities.Tokens;

namespace Spartacus.BusinessLogic.DBModel
{
    public class UserContext : DbContext
    {
        public UserContext() : 
            base("name=Spartacus") 
        {
        }

        public virtual DbSet<UTable> Users{ get; set; }
        public virtual DbSet<ResetToken> ResTokens { get; set; }
        public virtual DbSet<RegisterToken> RegTokens { get; set; }
        public virtual DbSet<MenDetTable> Details { get; set; }
        public virtual DbSet<SerTable> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<SerTable>()
                .HasMany(s => s.MenDetTables)
                .WithRequired(md => md.SerTable)
                .HasForeignKey(md => md.ServiceId);
        }
    }
}
