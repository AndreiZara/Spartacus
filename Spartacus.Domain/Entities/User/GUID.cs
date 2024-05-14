using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Entities.User
{
    public class GUID //Global Unique Identifier 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Unique Token")]
        public string Token { get; set; }

        [Display(Name = "Unique Token")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}
