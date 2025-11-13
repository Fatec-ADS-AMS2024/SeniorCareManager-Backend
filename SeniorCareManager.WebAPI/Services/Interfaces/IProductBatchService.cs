using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Services.Interfaces
{
    public interface IProductBatchService : IGenericService<ProductBatch, ProductBatchDTO>
    {
        new Task<ProductBatchDTO> GetById(int id);
        Task<ProductBatchDTO> GetById(long id);
        Task<IEnumerable<ProductBatchDTO>> GetByProduct(long productId);
        Task<IEnumerable<ProductBatchDTO>> GetExpiringBatches(int daysAhead = 30);
        Task<IEnumerable<ProductBatchDTO>> GetExpiredBatches();
        new Task Update(ProductBatchDTO entityDTO, int id);
        Task Update(ProductBatchDTO entityDTO, long id);
        new Task Remove(int id);
        Task Remove(long id);
    }
}
