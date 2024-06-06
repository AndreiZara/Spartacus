using Spartacus.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Entities.Tokens
{
    public class RegisterToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Display(Name = "Unique Token")]
        public string Token { get; set; }

        [Required]
        [Display(Name = "Unique Token")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public TokenStatus Status { get; set; }
    }
}
