using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spartacus.Domain.Entities.Location
{
    public class LTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Current no of visitors")]
        public int CurrentNoOfVisitors { get; set; }

        [Required]
        public int Capacity { get; set; }

        [Display(Name = "Monthly sales")]
        public int MonthlySales { get; set; }

        [Display(Name = "Aveage monthly sales")]
        public float AvgMonthlySales { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }
    }
}
