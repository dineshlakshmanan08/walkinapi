using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class Applicationsrole
{
    public int ApplicationsRolesId { get; set; }

    public int ApplicationsId { get; set; }

    public int RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Application Applications { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
