using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class Technology
{
    public int TechnologiesId { get; set; }

    public string Technology1 { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Userexpertisetech> Userexpertiseteches { get; set; } = new List<Userexpertisetech>();

    public virtual ICollection<Userfamiliartech> Userfamiliarteches { get; set; } = new List<Userfamiliartech>();
}
