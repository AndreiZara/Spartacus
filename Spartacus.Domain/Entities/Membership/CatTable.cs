using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spartacus.Domain.Entities.Membership
{
    public class CatTable
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public const string Month_12 = "12 Months";

        [Required]
        public const string Month_6 = "6 Months";

        [Required]
        public const string Month_3 = "3 Months";

        [Required]
        public const string Month_1 = "1 Month";

        [Required]
        public string Description { get; set; }

        [Required]
        public int Price_12;

        [Required]
        public int Price_6;

        [Required]
        public int Price_3;

        [Required]
        public int Price_1;


    }
}