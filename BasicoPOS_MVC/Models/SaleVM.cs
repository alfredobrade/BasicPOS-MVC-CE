using BasicPOS.Models;

namespace BasicoPOS_MVC.Models
{
    public class SaleVM
    {
        public int IdSale { get; set; }
        public string? SaleNumber { get; set; }
        public int? IdSaleDocumentType { get; set; }
        public string? SaleDocumentTypeName { get; set; } //nuevo
        public int? IdUser { get; set; }
        public string? User { get; set; }
        public string? ClientIdnumber { get; set; }
        public string? ClientName { get; set; }
        public string? SubTotal { get; set; } //string??
        public string? Taxes { get; set; } //string??
        public string? Total { get; set; } //string??
        public string? RegisterDate { get; set; } //string??
        public virtual ICollection<SaleProductVM> SaleProducts { get; set; }
    }
}
