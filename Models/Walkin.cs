using System;
using System.Collections.Generic;

namespace Walkin.Models;

public partial class Walkin
{
    public int WalkinId { get; set; }

    public string? Title { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Location { get; set; }

    public string? Internship { get; set; }

    public string? GeneralInstructions { get; set; }

    public string? InstructionsForExam { get; set; }

    public string? MinimumRequirements { get; set; }

    public string? Processes { get; set; }

    public string? WalkinVenue { get; set; }

    public string? ThingsToRemember { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<Walkinrole> Walkinroles { get; set; } = new List<Walkinrole>();
}
