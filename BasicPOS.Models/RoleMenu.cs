using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class RoleMenu
{
    public int IdRoleMenu { get; set; }

    public int? IdRole { get; set; }

    public int? IdMenu { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Role? IdRoleNavigation { get; set; }
}
