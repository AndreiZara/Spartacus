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
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Sorry, First name is too long.")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Sorry, Last name is too long.")]
        public string Lastname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Must be between 8 and 50 characters.")]
        public string Password { get; set; }

        [Display(Name = "Last login")]
        public DateTime LastLogin { get; set; }

        [Display(Name = "Last ip")]
        [StringLength(16)]
        public string LastIp { get; set; }

        [Display(Name = "Last username change")]
        [DisplayFormat(NullDisplayText = "No data")]
        public DateTime? LastUsernameChange { get; set; }

        public URole Level { get; set; }

        public virtual MsTable Membership { get; set; }

        [Display(Name = "Category Id")]
        [DisplayFormat(NullDisplayText = "No data")]
        public int? CatId { get; set; }

        [DisplayFormat(NullDisplayText = "No data")]
        public MsDuration? Period { get; set; }

        [Display(Name = "File name")]
        [DisplayFormat(NullDisplayText = "No data")]
        [StringLength(50)]
        public string FileName { get; set; }
    }
}
