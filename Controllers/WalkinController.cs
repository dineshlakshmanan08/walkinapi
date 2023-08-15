using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using Walkin.Models.Dto;
namespace Walkin.Controllers;

[ApiController]
[Route("[controller]")]
public class WalkinController : ControllerBase
{
    string Str = "server=localhost;port=3306;user=root;password=zeus@123;database=quantum;";
    [HttpPost]
    public IActionResult CreateWalkin([FromBody] WalkinDto walkin) {
        
       using var connection = new MySqlConnection(Str);
        try {
        connection.Open();
            using var transaction = connection.BeginTransaction();
              using var command = new MySqlCommand(
            "INSERT INTO walkin (Title, Start_date, End_date, Location, Internship, General_instructions, Instructions_for_exam, Minimum_requirements, Processes, Walkin_venue, Things_to_remember) VALUES( @Title,STR_TO_DATE(@StartDate, '%Y-%m-%d') , STR_TO_DATE(@EndDate, '%Y-%m-%d'), @Location, @Internship, @GeneralInstructions, @InstructionsForExam, @MinimumRequirements, @Processes, @WalkinVenue, @ThingsToRemember )", connection);
            
       
     command.Parameters.AddWithValue("@Title", walkin.Title);
    
        command.Parameters.AddWithValue("@StartDate",walkin.StartDate);
        command.Parameters.AddWithValue("@EndDate", walkin.EndDate);
    command.Parameters.AddWithValue("@Location",walkin.Location );
     command.Parameters.AddWithValue("@Internship",walkin.Internship );
      command.Parameters.AddWithValue("@GeneralInstructions",walkin.GeneralInstructions );
       command.Parameters.AddWithValue("@InstructionsForExam", walkin.InstructionsForExam);
        command.Parameters.AddWithValue("@MinimumRequirements",walkin.MinimumRequirements );
         command.Parameters.AddWithValue("@Processes", walkin.Processes);
          command.Parameters.AddWithValue("@WalkinVenue",walkin.WalkinVenue );
           command.Parameters.AddWithValue("@ThingsToRemember", walkin.ThingsToRemember);
          
        
  
 
        int rowsAffected = command.ExecuteNonQuery();



        if (rowsAffected > 0)
        {
            int lastInsertedId = Convert.ToInt32(command.LastInsertedId);
              Console.WriteLine(lastInsertedId);
              
            try{
                   
            
         int rowsAffected2 = 0;
        foreach (var row in walkin.walkinRole){
            Console.WriteLine(row);
             using var command2 = new MySqlCommand(
            "INSERT INTO walkinroles (Walkin_id,Role_id) VALUES (@walkinId,@roleId)", connection);
            command2.Parameters.AddWithValue("@walkinId", lastInsertedId);
             command2.Parameters.AddWithValue("@roleId", row);
              rowsAffected2 = command2.ExecuteNonQuery();
        }
   
       

        if (rowsAffected2 > 0)
        {
             transaction.Commit();
             return Ok("Walkin with role inserted successful");
       

    
        }else
        {
            transaction.Rollback();
            return BadRequest("User insertion failed.");
        }
        
        
            }
           
        catch(Exception ex){
            
            Console.WriteLine("asdff");
            return BadRequest(ex.Message);
        }
    }else
        {
            transaction.Rollback();
            return BadRequest("Walkin insertion failed.");
        }
} catch(Exception ex){
            
            Console.WriteLine("f");
            return BadRequest(ex.Message);
    }

    }
}