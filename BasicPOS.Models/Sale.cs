using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class Sale
{
    public int IdSale { get; set; }

    public string? SaleNumber { get; set; }

    public int? IdSaleDocumentType { get; set; }

    public int? IdUser { get; set; }

    public string? ClientIdnumber { get; set; }

    public string? ClientName { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Taxes { get; set; }

    public decimal? Total { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual SaleDocumentType? IdSaleDocumentTypeNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
}
