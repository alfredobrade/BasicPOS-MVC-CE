using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class SaleProduct
{
    public int IdSaleProduct { get; set; }

    public int? IdSale { get; set; }

    public int? IdProduct { get; set; }

    public int? Quantity { get; set; }

    public decimal? Cost { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Sale? IdSaleNavigation { get; set; }
}
