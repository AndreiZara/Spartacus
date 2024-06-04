using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spartacus.Domain.Entities.Services;

namespace Spartacus.Domain.Entities.User
{
    public class MenDetTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("SerTable")]
        public int ServiceId { get; set; }  

        public string Username { get; set; }

        [Required]
        [Display(Name = "Trainer description")]
        [StringLength(30)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Trainer activity")]
        [StringLength(30)]
        public string Activity { get; set; }

        public virtual SerTable SerTable { get; set; }
    }
}
