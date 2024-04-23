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
        public string Title { get; set; }

        //[Required]
        //public int[4] Prices { get; set; }

        [Required]
        public string Description { get; set; }

        //[Required]
        //public int[2] HourPeriod { get; set; }
    }
}
