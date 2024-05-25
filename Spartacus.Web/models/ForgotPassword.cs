using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Spartacus.Web.Models
{
    public class ForgotPassword
    {   
        [Display(Name = "Email")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "Email cannot be longer than 30 characters.")]
        public string Email { get; set; }
    }
}