using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class SaleDocumentType
{
    public int IdSaleDocumentType { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
