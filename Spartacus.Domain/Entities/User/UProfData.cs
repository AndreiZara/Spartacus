using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Spartacus.Domain.Entities.User
{
    public class UProfData
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

        [Display(Name = "File name")]
        [StringLength(50)]
        public string FileName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        // on post only
        public HttpPostedFileBase Image { get; set; }
    }
}
