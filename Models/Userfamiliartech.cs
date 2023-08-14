using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class Userfamiliartech
{
    public int UserFamiliarTechId { get; set; }

    public int UserId { get; set; }

    public int TechnologiesId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Technology Technologies { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
