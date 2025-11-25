namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    using SeniorCareManager.WebAPI.Objects.Enums;

    public class UserWithoutPasswordDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public UserStatus UserStatus { get; set; }
        public int? EmployeeId { get; set; }
    }
}
