using Spartacus.Domain.Enums;
using Spartacus.Web.Filters;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Spartacus.Web.Models
{
    public class UserUpdate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
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
        [Display(Name = "Email address")]
        [StringLength(30)]
        public string Email { get; set; }

        public URole Role { get; set; }

        [Display(Name = "Membership")]
        public int? CatId { get; set; }

        [Display(Name = "Period")]
        public MsDuration? Period { get; set; }

        [Display(Name = "Set membership?")]
        public bool SetMembership { get; set; }

        [RequiredIf("Role", URole.Trainer)]
        [StringLength(400)]
        public string Bio { get; set; }

        [RequiredIf("Role", URole.Trainer)]
        [StringLength(30)]
        public string Activity { get; set; }

        [StringLength(64)]
        [Display(Name = "Instagram url")]
        public string InstagramUrl { get; set; }

        [StringLength(64)]
        [Display(Name = "Facebook url")]
        public string FacebookUrl { get; set; }

        // readonly
        [Display(Name = "File name")]
        [StringLength(50)]
        public string FileName { get; set; }

        public SelectList Categories { get; set; }
    }
}