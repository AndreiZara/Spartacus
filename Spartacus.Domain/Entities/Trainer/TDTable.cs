using Spartacus.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spartacus.Domain.Entities.Trainer
{
    public class TDTable
    {
        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        [StringLength(400)]
        public string Bio { get; set; }

        [Required]
        [StringLength(30)]
        public string Activity { get; set; }

        [StringLength(64)]
        public string InstagramUrl { get; set; }

        [StringLength(64)]
        public string FacebookUrl { get; set; }

        [Required]
        public virtual UTable User { get; set; }
    }
}
