using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Spartacus.Domain.Enums;

namespace Spartacus.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "FIrstname cannot be longer than 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Firstname cannot be longer than 30 characters.")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Lastname")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Lastname cannot be longer than 30 characters.")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        [StringLength(30)]
        public string LastIp { get; set; }

        public URole Level { get; set; }
    }
}
