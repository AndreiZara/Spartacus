using System;
using System.ComponentModel.DataAnnotations;

namespace Spartacus.Domain.Entities.User
{
    public class FeedData
    {
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

        public DateTime DateSent { get; set; }
    }
}
