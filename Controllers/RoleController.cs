using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using Walkin.Models.Dto;

namespace Walkin.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    string Str = "server=localhost;port=3306;user=root;password=zeus@123;database=quantum;";

    [HttpPost]
    public IActionResult CreateRole([FromBody] RoleDto role)
    {
        using var connection = new MySqlConnection(Str);
        try
        {
            connection.Open();
            using var command = new MySqlCommand(
                "INSERT INTO roles (Role_name, Package,Descriptions,Requirements) VALUES (@Role_name, @Package,@Descriptions,@Requirements)",
                connection
            );

            command.Parameters.AddWithValue("@Role_name", role.RoleName);
            command.Parameters.AddWithValue("@Package", role.Package);
            command.Parameters.AddWithValue("@Descriptions", role.Descriptions);
            command.Parameters.AddWithValue("@Requirements", role.Requirements);
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
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
