using System.ComponentModel.DataAnnotations;

namespace Spartacus.Web.Models
{
    public class UserRegister
    {
        public UserRegister(UserRegister register)
        {
            Username = register.Username;
            Firstname = register.Firstname;
            Lastname = register.Lastname;
            Email = register.Email;
            Password = register.Password;
        }
        public UserRegister() { }

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

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 5 and 50 characters.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords should be the same.")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}