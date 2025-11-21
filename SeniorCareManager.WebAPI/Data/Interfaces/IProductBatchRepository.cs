using SeniorCareManager.WebAPI.Objects.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorCareManager.WebAPI.Data.Interfaces
{
    public interface IProductBatchRepository : IGenericRepository<ProductBatch>
    {
        Task<IEnumerable<ProductBatch>> GetByProduct(long productId);
        Task<IEnumerable<ProductBatch>> GetExpiringBatchesAsync(DateTime cutoffDate);
        Task<IEnumerable<ProductBatch>> GetExpiredBatchesAsync();
        Task<bool> ExistsByNumberForProductAsync(string batchNumber, long productId, long? currentBatchId = null);
        Task<ProductBatch> GetById(long id);
    }
}