using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class Application
{
    public int ApplicationsId { get; set; }

    public int UserId { get; set; }

    public int WalkinId { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Applicationsrole> Applicationsroles { get; set; } = new List<Applicationsrole>();

    public virtual User User { get; set; } = null!;

    public virtual Walkin Walkin { get; set; } = null!;
}
