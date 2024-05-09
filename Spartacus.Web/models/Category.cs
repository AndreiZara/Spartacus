namespace Spartacus.Web.Models
{
    public class Category
    {
        // represents hashed value of or id or title
        public string Val { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PriceOneYear { get; set; }
        public int PriceSixMonths { get; set; }
        public int PriceThreeMonths { get; set; }
        public int PriceOneMonth { get; set; }
    }
}