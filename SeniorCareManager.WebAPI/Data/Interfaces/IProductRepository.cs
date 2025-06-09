using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
         Task<Product> GetById(long id);

    }
}
