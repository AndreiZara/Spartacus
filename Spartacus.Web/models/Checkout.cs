using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Spartacus.Web.Models
{
    public class Checkout
    {
        [Required]
        [Display(Name = "Name on card")]
        public string NameOnCard { get; set; }

        [CreditCard]
        [Required]
        [Display(Name = "Card number")]
        public string CardNumber { get; set; }

        [Required]
        public string Expiry { get; set; }

        [Required]
        public string CVV { get; set; }

        [Required]
        [Display(Name = "Street address")]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocId { get; set; }

        // Read only
        public int Price { get; set; }
        public DateTime EndTime { get; set; }
        //public int CatId { get; set; }
        public SelectList Locations { get; set; }

    }
}