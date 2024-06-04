using Spartacus.Domain.Entities.Tokens;
using System.Data.Entity;

namespace Spartacus.BusinessLogic.DBModel
{
    public class TokenContext : DbContext
    {
        public TokenContext() : base("name=Spartacus") { }

        public virtual DbSet<ResetToken> ResetTokens { get; set; }
        public virtual DbSet<AccessToken> AccessTokens { get; set; }
    }
}