using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using Walkin.Models.Dto;
using System.Security.Cryptography;
using System.Text;
namespace Walkin.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    string Str = "server=localhost;port=3306;user=root;password=zeus@123;database=quantum;";
    [HttpPost]
    public IActionResult CreateUser([FromBody] UserDto user) {
        
       using var connection = new MySqlConnection(Str);
        try {
        connection.Open();
            using var transaction = connection.BeginTransaction();
              using var command = new MySqlCommand(
            "INSERT INTO users (gui_id,Email_id,Password_user, First_name, Last_name, Phone_no, Portfolio, Resume_user, Aggregate_percentage, Passing_year, Qualification, Stream_user, College, College_location, Applicant_type, Experience, Current_Ctc, Expected_Ctc, Notice_period, Notice_period_end_date, Notice_period_duration_in_month, Applied_in_zeus, Applied_role_in_zeus, profile_image) VALUES(@guid,@email,@password,@fname,@lname,@phoneno,@portfolio,@resume,@agg,@passing,@qualification,@stream,@college,@collegeLocation,@applicantType,@experience,@currentCtc,@expectedCtc,@noticePeriod,STR_TO_DATE(@noticePeriodEndDate, '%Y-%m-%d') ,@noticePeriodMonth,@applied,@appliedRole,@profileImage)", connection);
            
        command.Parameters.AddWithValue("@guid", Guid.NewGuid());
     command.Parameters.AddWithValue("@email", user.EmailId);
     string hashedPassword = "";
            using (SHA256 sha256 = SHA256.Create())
            {
               
                byte[] inputBytes = Encoding.UTF8.GetBytes(user.PasswordUser);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

              
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
               hashedPassword = builder.ToString();
            }
        command.Parameters.AddWithValue("@password",hashedPassword);
        command.Parameters.AddWithValue("@fname", user.FirstName);
    command.Parameters.AddWithValue("@lname",user.LastName );
     command.Parameters.AddWithValue("@phoneno",user.PhoneNo );
      command.Parameters.AddWithValue("@portfolio",user.Portfolio );
       command.Parameters.AddWithValue("@resume", user.ResumeUser);
        command.Parameters.AddWithValue("@agg",user.AggregatePercentage );
         command.Parameters.AddWithValue("@passing", user.PassingYear);
          command.Parameters.AddWithValue("@qualification",user.Qualification );
           command.Parameters.AddWithValue("@stream", user.StreamUser);
            command.Parameters.AddWithValue("@college", user.College);
         command.Parameters.AddWithValue("@collegeLocation",user.CollegeLocation );
          command.Parameters.AddWithValue("@applicantType",user.ApplicantType );
           command.Parameters.AddWithValue("@experience",user.Experience );
            command.Parameters.AddWithValue("@currentCtc",user.CurrentCtc );
             command.Parameters.AddWithValue("@expectedCtc",user.ExpectedCtc );
              command.Parameters.AddWithValue("@noticePeriod",user.NoticePeriod );
        
  
              
                        // Format DateTime in MySQL-compatible format
                
                        command.Parameters.AddWithValue("@noticePeriodEndDate", user.NoticePeriodEndDate);
    
          


            //    command.Parameters.AddWithValue("@noticePeriodEndDate",user.NoticePeriodEndDate );
                command.Parameters.AddWithValue("@noticePeriodMonth",user.NoticePeriodDurationInMonth );
                 command.Parameters.AddWithValue("@applied", user.AppliedInZeus);
                  command.Parameters.AddWithValue("@appliedRole",user.AppliedRoleInZeus);
                   command.Parameters.AddWithValue("@profileImage",user.ProfileImage );
        int rowsAffected = command.ExecuteNonQuery();



        if (rowsAffected > 0)
        {
            int lastInsertedId = Convert.ToInt32(command.LastInsertedId);
              Console.WriteLine(lastInsertedId);
              
            try{
                   
            
         int rowsAffected2 = 0;
        foreach (var row in user.Userpreferredrole){
            Console.WriteLine(row);
             using var command2 = new MySqlCommand(
            "INSERT INTO userpreferredroles (User_id,Role_id) VALUES (@userId,@roleId)", connection);
            command2.Parameters.AddWithValue("@userId", lastInsertedId);
             command2.Parameters.AddWithValue("@roleId", row);
              rowsAffected2 = command2.ExecuteNonQuery();
        }
   
       

        if (rowsAffected2 > 0)
        {
             try{
                   
            
         int rowsAffected3 = 0;
        foreach (var row in user.Userfamiliartech){
            Console.WriteLine(row);
             using var command3 = new MySqlCommand(
            "INSERT INTO userfamiliartech (User_id,Technologies_id) VALUES (@userId,@technologiesId)", connection);
            command3.Parameters.AddWithValue("@userId", lastInsertedId);
             command3.Parameters.AddWithValue("@technologiesId", row);
              rowsAffected3 = command3.ExecuteNonQuery();
        }
   
       

        if (rowsAffected3 > 0)
        {
             try{
                   
            
         int rowsAffected4 = 0;
        foreach (var row in user.Userexpertisetech){
            Console.WriteLine(row);
             using var command4 = new MySqlCommand(
            "INSERT INTO userexpertisetech (User_id,Technologies_id) VALUES (@userId,@technologiesId)", connection);
            command4.Parameters.AddWithValue("@userId", lastInsertedId);
             command4.Parameters.AddWithValue("@technologiesId", row);
              rowsAffected4 = command4.ExecuteNonQuery();
        }
   
       

        if (rowsAffected4 > 0)
        {
            transaction.Commit();
            return Ok("Successfully inserted everywhere");
        }
        else
        {
             transaction.Rollback();
            return BadRequest("Failed to insert Technology.");
        }
            }catch(Exception ex){
                 Console.WriteLine("asdffh");
                return BadRequest(ex.Message);
            }
        }
        else
        {
             transaction.Rollback();
            return BadRequest("Failed to insert Technology.");
        }
            }catch(Exception ex){
                 Console.WriteLine("asdffh");
                return BadRequest(ex.Message);
            }
        }
        else
        {
             transaction.Rollback();
            return BadRequest("Failed to insert Technology.");
        }
            }catch(Exception ex){
                 Console.WriteLine("asdffh");
                return BadRequest(ex.Message);
            }
           
           
           
        }
        else
        {
            transaction.Rollback();
            return BadRequest("User insertion failed.");
        }
        
        
            }
           
        catch(Exception ex){
            
            Console.WriteLine("asdff");
            return BadRequest(ex.Message);
        }
    }
}
