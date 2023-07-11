using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class Category
{
    public int IdCategory { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
