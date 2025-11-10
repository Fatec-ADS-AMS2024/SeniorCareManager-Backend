using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces
{
    public interface IAllergyRepository : IGenericRepository<Allergy>
    {
        Task<bool> ExistsByNameAsync(string name, int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
