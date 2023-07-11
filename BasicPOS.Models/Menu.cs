using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string? Description { get; set; }

    public int? IdMenuPadre { get; set; }

    public string? Icon { get; set; }

    public string? Controller { get; set; }

    public string? ActionPage { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual Menu? IdMenuPadreNavigation { get; set; }

    public virtual ICollection<Menu> InverseIdMenuPadreNavigation { get; set; } = new List<Menu>();

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
