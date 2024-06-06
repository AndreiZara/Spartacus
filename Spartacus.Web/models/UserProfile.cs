using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using System;
using System.ComponentModel.DataAnnotations;

namespace Spartacus.Web.Models
{
    public class UserProfile
    {
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Sorry, First name too long.")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Sorry, Last name too long.")]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string Password { get; set; }

        [RequiredIf("Role", URole.Trainer)]
        [StringLength(30, ErrorMessage = "Sorry, your activity is too long.")]
        public string Activity { get; set; }

        [RequiredIf("Role", URole.Trainer)]
        [StringLength(400, ErrorMessage = "Sorry, your bio is too long.")]
        public string Bio { get; set; }

        [StringLength(64)]
        [Display(Name = "Instagram url")]
        public string InstagramUrl { get; set; }

        [StringLength(64)]
        [Display(Name = "Facebook url")]
        public string FacebookUrl { get; set; }

        // read only
        public string FileName { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndTime { get; set; }
        public int RemainingDays { get; set; }
        public URole Role { get; set; }
        // and something related to category
        public string Title { get; set; }
        public string Description { get; set; }
    }
}