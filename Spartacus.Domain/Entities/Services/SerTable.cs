using Spartacus.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web;

namespace Spartacus.Domain.Entities.Services
{
    public class SerTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required]
        [Display(Name = "Service Title")]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Service description")]
        [StringLength(30)]
        public string Description { get; set; }

        [NotMapped]
        [Display(Name = "File")]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "File name")]
        public string FileName { get; set; }

        [Display(Name = "Related trainers")]
        public virtual ICollection<MenDetTable> MenDetTables { get; set; }
    }
}
