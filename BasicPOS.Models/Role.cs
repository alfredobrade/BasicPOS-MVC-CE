using System;
using System.Collections.Generic;

namespace BasicPOS.Models;

public partial class Role
{
    //TODO: esto tenia el vafo tambien-- que mierda es?
    public Role()
    {
        RoleMenus = new HashSet<RoleMenu>();
        Users = new HashSet<User>();
    }

    //esto es lo que me trajo el scaffolding
    public int IdRole { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? RegisterDate { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
