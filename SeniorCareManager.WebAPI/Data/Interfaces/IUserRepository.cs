using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmail(string email);
}