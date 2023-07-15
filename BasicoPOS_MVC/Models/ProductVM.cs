using BasicPOS.Models;

namespace BasicoPOS_MVC.Models
{
    public class ProductVM
    {
        public int IdProduct { get; set; }
        public string? BarrCode { get; set; }
        public string? ProductBrand { get; set; }
        public string? Description { get; set; }
        public int? IdCategory { get; set; }
        public string? CategoryName { get; set; } //nuevo
        public int? Stock { get; set; }
        public int? MinimumStock { get; set; }
        public string? ImgUrl { get; set; }
        public string? ImgName { get; set; }
        public decimal? Cost { get; set; }
        public string? Price { get; set; } //string???
        public int? IsActive { get; set; } //paso de bool a int

    }
}
