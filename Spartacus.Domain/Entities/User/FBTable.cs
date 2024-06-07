using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spartacus.Domain.Entities.User
{
    public class FBTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Your name")]
        [StringLength(41, MinimumLength = 1, ErrorMessage = "Sorry, your name is too long.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Your message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Date sent")]
        public DateTime DateSent { get; set; }
    }
}
