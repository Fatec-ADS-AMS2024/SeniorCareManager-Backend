using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
    public class ProductBatchRepository : GenericRepository<ProductBatch>, IProductBatchRepository
    {
        private readonly AppDbContext _context;

        public ProductBatchRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductBatch> GetById(long id)
        {
            return await _context.Set<ProductBatch>().FindAsync(id);
        }

        public async Task<IEnumerable<ProductBatch>> GetByProduct(long productId)
        {
            return await _context.Set<ProductBatch>().Where(b => b.ProductId == productId).ToListAsync();
        }

        public async Task<IEnumerable<ProductBatch>> GetExpiringBatchesAsync(DateTime cutoffDate)
        {
            return await _context.Set<ProductBatch>()
                .Where(b => b.ExpirationDate <= cutoffDate && b.ExpirationDate > DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductBatch>> GetExpiredBatchesAsync()
        {
            return await _context.Set<ProductBatch>()
                .Where(b => b.ExpirationDate < DateTime.UtcNow)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNumberForProductAsync(string batchNumber, long productId, long? currentBatchId = null)
        {
            var query = _context.Set<ProductBatch>()
                .Where(b => b.ProductId == productId && b.BatchNumber.ToLower() == batchNumber.ToLower());

            if (currentBatchId.HasValue)
            {
                query = query.Where(b => b.Id != currentBatchId.Value);
            }

            return await query.AnyAsync();
        }
    }
}