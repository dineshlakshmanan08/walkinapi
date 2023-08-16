using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using Walkin.Models.Dto;

namespace Walkin.Controllers;

[ApiController]
[Route("[controller]")]
public class VerifyController : ControllerBase
{
    string Str = "server=localhost;port=3306;user=root;password=zeus@123;database=quantum;";

    [HttpPost]
    public IActionResult Verify([FromBody] VerifyDto verify)
    {
        using var connection = new MySqlConnection(Str);
        try
        {
            connection.Open();

            using var command = new MySqlCommand(
                "Select Email_id from users Where Email_id = @Email",
                connection
            );
            command.Parameters.AddWithValue("@Email", verify.Email);
            int rowCount = 0;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    rowCount++;
                }
            }

            if (rowCount > 0)
            {
                string hashedPassword = "";
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(verify.Password);
                    byte[] hashBytes = sha256.ComputeHash(inputBytes);

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        builder.Append(hashBytes[i].ToString("x2"));
                    }
                    hashedPassword = builder.ToString();
                }
                using var commands = new MySqlCommand(
                    "Select Password_user,gui_id from users Where Password_user = @Password && Email_id = @Email",
                    connection
                );
                commands.Parameters.AddWithValue("@Password", hashedPassword);
                commands.Parameters.AddWithValue("@Email", verify.Email);
                int rowCountP = 0;
                string guid_id = "";
                using (MySqlDataReader reader = commands.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rowCountP++;
                        guid_id = reader.GetString(1);
                    }
                }

                if (rowCountP > 0)
                {
                    return Ok(guid_id);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
            else
            {
                return BadRequest("Noo user.Please Register");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
