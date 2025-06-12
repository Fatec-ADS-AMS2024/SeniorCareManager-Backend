using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces;

public interface IProductGroupRepository : IGenericRepository<ProductGroup>
{
    Task<bool> ExistsAsync(int id);
}