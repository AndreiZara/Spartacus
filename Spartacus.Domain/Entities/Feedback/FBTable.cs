using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Entities.Feedback
{
    public class FBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }

        public string AdminUsername { get; set; }

        [Required]
        [Display(Name = "Email")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Email cannot be longer than 50 characters.")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Subject")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Subject cannot be longer than 50 characters.")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Message")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Message cannot be longer than 100 characters.")]
        public string Message { get; set; }

    }
}
