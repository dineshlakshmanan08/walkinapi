using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class User
{
    public int UserId { get; set; }

    public string GuiId { get; set; } = null!;

    public string? EmailId { get; set; }

    public string? PasswordUser { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNo { get; set; }

    public string? Portfolio { get; set; }

    public string? ResumeUser { get; set; }

    public int? AggregatePercentage { get; set; }

    public int? PassingYear { get; set; }

    public string? Qualification { get; set; }

    public string? StreamUser { get; set; }

    public string? College { get; set; }

    public string? CollegeLocation { get; set; }

    public string? ApplicantType { get; set; }

    public int? Experience { get; set; }

    public int? CurrentCtc { get; set; }

    public int? ExpectedCtc { get; set; }

    public bool? NoticePeriod { get; set; }

    public DateOnly? NoticePeriodEndDate { get; set; }

    public int? NoticePeriodDurationInMonth { get; set; }

    public bool? AppliedInZeus { get; set; }

    public string? AppliedRoleInZeus { get; set; }

    public string? ProfileImage { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Userexpertisetech> Userexpertiseteches { get; set; } = new List<Userexpertisetech>();

    public virtual ICollection<Userfamiliartech> Userfamiliarteches { get; set; } = new List<Userfamiliartech>();

    public virtual ICollection<Userpreferredrole> Userpreferredroles { get; set; } = new List<Userpreferredrole>();
}
