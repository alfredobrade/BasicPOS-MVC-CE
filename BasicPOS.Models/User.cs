using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public string? PhoneNumber { get; set; }

    public int? IdRole { get; set; }

    public string? PhotoUrl { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
