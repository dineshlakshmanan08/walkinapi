using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class Walkinrole
{
    public int WalkInRolesId { get; set; }

    public int WalkinId { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Walkin Walkin { get; set; } = null!;
}
