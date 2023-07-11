using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class SaleNumber
{
    public int IdSaleNumber { get; set; }

    public int? LastNumber { get; set; }

    public int? DigitQty { get; set; }

    public string? Management { get; set; }

    public DateTime? UpdateDate { get; set; }
}
