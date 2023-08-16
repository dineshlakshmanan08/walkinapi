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
    public IActionResult CreateWalkin([FromBody] WalkinDto walkin)
    {
        using var connection = new MySqlConnection(Str);
        try
        {
            connection.Open();
            using var transaction = connection.BeginTransaction();
            using var command = new MySqlCommand(
                "INSERT INTO walkin (Title, Start_date, End_date, Location, Internship, General_instructions, Instructions_for_exam, Minimum_requirements, Processes, Walkin_venue, Things_to_remember) VALUES( @Title,STR_TO_DATE(@StartDate, '%Y-%m-%d') , STR_TO_DATE(@EndDate, '%Y-%m-%d'), @Location, @Internship, @GeneralInstructions, @InstructionsForExam, @MinimumRequirements, @Processes, @WalkinVenue, @ThingsToRemember )",
                connection
            );

            command.Parameters.AddWithValue("@Title", walkin.Title);

            command.Parameters.AddWithValue("@StartDate", walkin.StartDate);
            command.Parameters.AddWithValue("@EndDate", walkin.EndDate);
            command.Parameters.AddWithValue("@Location", walkin.Location);
            command.Parameters.AddWithValue("@Internship", walkin.Internship);
            command.Parameters.AddWithValue("@GeneralInstructions", walkin.GeneralInstructions);
            command.Parameters.AddWithValue("@InstructionsForExam", walkin.InstructionsForExam);
            command.Parameters.AddWithValue("@MinimumRequirements", walkin.MinimumRequirements);
            command.Parameters.AddWithValue("@Processes", walkin.Processes);
            command.Parameters.AddWithValue("@WalkinVenue", walkin.WalkinVenue);
            command.Parameters.AddWithValue("@ThingsToRemember", walkin.ThingsToRemember);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                int lastInsertedId = Convert.ToInt32(command.LastInsertedId);

                try
                {
                    int rowsAffected2 = 0;
                    foreach (var row in walkin.walkinRole)
                    {
                        using var command2 = new MySqlCommand(
                            "INSERT INTO walkinroles (Walkin_id,Role_id) VALUES (@walkinId,@roleId)",
                            connection
                        );
                        command2.Parameters.AddWithValue("@walkinId", lastInsertedId);
                        command2.Parameters.AddWithValue("@roleId", row);
                        rowsAffected2 = command2.ExecuteNonQuery();
                    }

                    if (rowsAffected2 > 0)
                    {
                        transaction.Commit();
                        return Ok("Walkin with role inserted successful");
                    }
                    else
                    {
                        transaction.Rollback();
                        return BadRequest("User insertion failed.");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                transaction.Rollback();
                return BadRequest("Walkin insertion failed.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public IActionResult getWalkins()
    {
        using var connection = new MySqlConnection(Str);
        try
        {
            connection.Open();
            using var command = new MySqlCommand(
                "SELECT w.*,r.* FROM walkin w LEFT JOIN walkinroles wr ON w.Walkin_id = wr.Walkin_id LEFT JOIN roles r ON r.Role_id = wr.Role_id",
                connection
            );

            using MySqlDataReader reader = command.ExecuteReader();
            var walkins = new List<WalkinDt>();

            while (reader.Read())
            {
                var walkin = new WalkinDt
                {
                    Title = reader.GetString("Title"),
                    GeneralInstructions = reader.GetString("General_instructions"),
                    StartDate = reader.GetString("Start_date"),
                    EndDate = reader.GetString("End_date"),
                    Location = reader.GetString("Location"),
                    Internship = reader.GetString("Internship"),
                    InstructionsForExam = reader.GetString("Instructions_for_exam"),
                    MinimumRequirements = reader.GetString("Minimum_requirements"),
                    Processes = reader.GetString("Processes"),
                    WalkinVenue = reader.GetString("Walkin_venue"),
                    ThingsToRemember = reader.GetString("Things_to_remember"),
                    walkinRole = reader.GetInt32("Role_id"),
                    Package = reader.GetInt32("Package"),
                    RoleName = reader.GetString("Role_name"),
                    Descriptions = reader.GetString("Descriptions"),
                    Requirements = reader.GetString("Requirements"),
                };
                walkins.Add(walkin);
            }

            return Ok(walkins);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                return Ok("Walkin Fetched  successfully.");
            }
            else
            {
                return BadRequest("Failed to fetch walkin.");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
