using Spartacus.Domain.Entities.Membership;
using Spartacus.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spartacus.Domain.Entities.User
{
    public class UTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string Password { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        [StringLength(16)]
        public string LastIp { get; set; }

        public URole Level { get; set; }

        public virtual MsTable Membership { get; set; }
    }
}
