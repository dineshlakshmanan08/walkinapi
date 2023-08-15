using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using Walkin.Models.Dto;
using System.Globalization;
namespace Walkin.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    string Str = "server=localhost;port=3306;user=root;password=zeus@123;database=quantum;";
    [HttpPost("{userId}/walkin/{walkinId}")]
    public IActionResult CreateWalkin([FromRoute] int userId, int walkinId,[FromBody] ApplicationDto application) {
        
       using var connection = new MySqlConnection(Str);
        try {
        connection.Open();
            using var transaction = connection.BeginTransaction();
              using var command = new MySqlCommand(
            "INSERT INTO applications (User_id,Walkin_id,Start_time,End_time) VALUES(@userId,@walkinId,@startTime ,@endTime)", connection);
            Console.WriteLine(application.StartTime);
         Console.WriteLine(userId);
     command.Parameters.AddWithValue("@userId",userId);
    
        command.Parameters.AddWithValue("@walkinId",walkinId);
 
                    command.Parameters.AddWithValue("@startTime", application.StartTime);
                    command.Parameters.AddWithValue("@endTime", application.EndTime);
           
            


          
        
  
 
        int rowsAffected = command.ExecuteNonQuery();



        if (rowsAffected > 0)
        {
            int lastInsertedId = Convert.ToInt32(command.LastInsertedId);
              Console.WriteLine(lastInsertedId);
              
            try{
                   
            
         int rowsAffected2 = 0;
        foreach (var row in application.applicationRole){
            Console.WriteLine(row);
             using var command2 = new MySqlCommand(
            "INSERT INTO applicationsroles (Applications_id,Role_id) VALUES (@applicationId,@roleId)", connection);
            command2.Parameters.AddWithValue("@applicationId", lastInsertedId);
             command2.Parameters.AddWithValue("@roleId", row);
              rowsAffected2 = command2.ExecuteNonQuery();
        }
   
       

        if (rowsAffected2 > 0)
        {
             transaction.Commit();
             return Ok("Application with role inserted successful");
       

    
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
            return BadRequest("Application insertion failed.");
        }
} catch(Exception ex){
            
            Console.WriteLine("g");
            return BadRequest(ex.Message);
    }

    }
}