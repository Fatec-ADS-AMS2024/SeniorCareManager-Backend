using SeniorCareManager.WebAPI.Objects.Dtos.Entities;

namespace SeniorCareManager.WebAPI.Services.Interfaces;

public interface IUserService : IGenericService<User, UserDTO>
{
    Task<UserDTO> GetByEmail(string email);
}