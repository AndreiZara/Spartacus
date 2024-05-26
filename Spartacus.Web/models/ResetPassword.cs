using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 5 and 50 characters.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords should be the same.")]
        [Display(Name = "Confirm new password")]
        public string ConfirmNewPassword { get; set; }
    }
}