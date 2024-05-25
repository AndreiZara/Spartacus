using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.DBModel
{
    public class TokenContext : DbContext
    {
        public TokenContext() : base("name=Spartacus") { }

        public virtual DbSet<ResetToken> ResetTokens { get; set; }
    }
}