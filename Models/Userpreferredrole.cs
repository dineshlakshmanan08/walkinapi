using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class Userpreferredrole
{
    public int UserPreferredRolesId { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
