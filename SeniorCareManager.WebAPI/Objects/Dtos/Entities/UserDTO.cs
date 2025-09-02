using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class UserDTO
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
    public UserStatus UserStatus { get; set; }
}