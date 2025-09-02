using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("user")]
public class User
{
    [Column("id")]
    public int Id { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("usertype")]
    public UserType UserType { get; set; }
    
    [Column("userstatus")]
    public UserStatus UserStatus { get; set; }

    [JsonIgnore]
    public ICollection<Employee>? Employees { get; set; }

    public User()
    {
        
    }

    public User(int id, string email, string password, UserType userType, UserStatus userStatus)
    {
        Id = id;
        Email = email;
        Password = password;
        UserType = userType;
        UserStatus = userStatus;
    }
}
