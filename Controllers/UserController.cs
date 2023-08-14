using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using Walkin.Models;
namespace Walkin.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    string Str = "server=localhost;port=3306;user=root;password=zeus@123;database=quantum;";
    [HttpPost]
    public IActionResult Create([FromBody] User user) {
         Console.WriteLine("asdf");
       using var connection = new MySqlConnection(Str);
        try {
        
               
                
                connection.Open();
                
              using var command = new MySqlCommand(
            "INSERT INTO roles (User_id,gui_id,First_Name) VALUES ('asdf','ddsadf',@name)", connection);
            
        command.Parameters.AddWithValue("@name", user.FirstName);
        Console.WriteLine(user.FirstName);
        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            return Ok("User inserted successfully.");
        }
        else
        {
            return BadRequest("Failed to insert user.");
        }
         
            }
           
        catch(Exception ex){
            Console.WriteLine("asdf");
            return BadRequest(ex.Message);
        }
    }
}
