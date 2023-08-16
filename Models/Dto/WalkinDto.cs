namespace Walkin.Models.Dto;
public class WalkinDto{
     
    public string Title { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }

    public string Location { get; set; }

    public string Internship { get; set; }

    public string GeneralInstructions { get; set; }

    public string InstructionsForExam { get; set; }
    public string MinimumRequirements { get; set; }

    public string Processes { get; set; }

    public string WalkinVenue { get; set; }

    public string ThingsToRemember { get; set; }

    public List<int> walkinRole {get; set;}

 
}