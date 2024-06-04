using System.ComponentModel.DataAnnotations;

namespace Spartacus.Web.Models
{
    public class ResetPassword
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "Passwords should be the same.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 5 and 50 characters.")]
        public string ConfirmNewPassword { get; set; }
    }
}