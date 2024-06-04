using Spartacus.Domain.Entities.Feedback;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.BusinessLogic.DBModel
{
    public class FeedbackContext:DbContext
    {
        public FeedbackContext() : base("name=Spartacus")
        {
        }

        public DbSet<FBTable> FBTables { get; set; }
    }
}
