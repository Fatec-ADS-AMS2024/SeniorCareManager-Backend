using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly AppDbContext _context;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(AppDbContext context) : base(context)
		{
			this._context = context;
            this._dbSet = _context.Set<Product>();
        }
        public async Task<Product> GetById(long id)
        {
            return await _dbSet.FindAsync(id);
        }
    }

}
