using Spartacus.Domain.Entities.User;
using Spartacus.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spartacus.Domain.Entities.Membership
{
    public class MsTable
    {
        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Category Id")]
        public int CatId { get; set; }

        [Required]
        [Display(Name = "Location Id")]
        public int LocId { get; set; }

        [Required]
        public MsDuration Period { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }

        [Required]
        public virtual UTable User { get; set; }
    }
}