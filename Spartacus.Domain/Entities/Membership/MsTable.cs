using Spartacus.Domain.Entities.User;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spartacus.Domain.Entities.Membership
{
    public class MsTable
    {
        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public virtual UTable User { get; set; }
    }
}