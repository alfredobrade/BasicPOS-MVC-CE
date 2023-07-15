using BasicPOS.Models;

namespace BasicoPOS_MVC.Models
{
    public class SaleProductVM //este cambio todo
    {
        public int? IdProduct { get; set; }
        public string? ProductBrand { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public int? Quantity { get; set; }
        public decimal? Cost { get; set; }
        public string? Price { get; set; }
        public string? Total { get; set; }

    }
}
