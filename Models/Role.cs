using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Walkin.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public int? Package { get; set; }

    public string? Descriptions { get; set; }

    public string? Requirements { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    
    public virtual ICollection<Applicationsrole> Applicationsroles { get; set; } = new List<Applicationsrole>();
   
    public virtual ICollection<Userpreferredrole> Userpreferredroles { get; set; } = new List<Userpreferredrole>();

    public virtual ICollection<Walkinrole> Walkinroles { get; set; } = new List<Walkinrole>();
}
