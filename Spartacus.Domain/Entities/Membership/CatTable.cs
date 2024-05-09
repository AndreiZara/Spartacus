using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spartacus.Domain.Entities.Membership
{
    public class CatTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public int PriceOneMonth { get; set; }

        [Required]
        public int PriceThreeMonths { get; set; }

        [Required]
        public int PriceSixMonths { get; set; }

        [Required]
        public int PriceOneYear { get; set; }
    }
}