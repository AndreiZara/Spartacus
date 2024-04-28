using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Entities.Membership
{
    public class CategoryTable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Price for 12 month")]
        public string Price_12 { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Price for 6 month")]
        public string Price_6 { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Price for 3 month")]
        public string Price_3 { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Price for 1 month")]
        public string Price_1 { get; set; }


    }
}