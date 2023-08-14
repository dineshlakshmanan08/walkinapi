using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using Walkin.Models.Dto;
namespace Walkin.Controllers;

[ApiController]
[Route("[controller]")]
public class TechnologyController : ControllerBase
{
    string Str = "server=localhost;port=3306;user=root;password=zeus@123;database=quantum;";
    [HttpPost]
    public IActionResult CreateTechnology([FromBody] TechnologyDto tech) {

        using var connection = new MySqlConnection(Str);
        try {
          
                connection.Open();
              using var command = new MySqlCommand(
            "INSERT INTO technologies (Technology) VALUES (@Techn)", connection);
            
        command.Parameters.AddWithValue("@Techn", tech.Technology1);
  
        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            return Ok("Technology inserted successfully.");
        }
        else
        {
            
            return BadRequest("Failed to insert Technology.");
        }
            
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }
}