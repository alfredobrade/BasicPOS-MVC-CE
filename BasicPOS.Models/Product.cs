using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string? BarrCode { get; set; }

    public string? ProductBrand { get; set; }

    public string? Description { get; set; }

    public int? IdCategory { get; set; }

    public int? Stock { get; set; }

    public int? MinimumStock { get; set; }

    public string? ImgUrl { get; set; }

    public string? ImgName { get; set; }

    public decimal? Cost { get; set; }

    public decimal? Price { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual Category? IdCategoryNavigation { get; set; }

    public virtual ICollection<SaleProduct> SaleProducts { get; set; } = new List<SaleProduct>();
}
