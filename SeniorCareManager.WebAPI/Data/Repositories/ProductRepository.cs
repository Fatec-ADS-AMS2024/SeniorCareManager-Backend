using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories;

public class ProductRepository : GenericRepository<ProductGroup>, IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }

    public Task Add(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task Remove(Product entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Product entity)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Product>> IGenericRepository<Product>.Get()
    {
        throw new NotImplementedException();
    }

    Task<Product> IGenericRepository<Product>.GetById(int id)
    {
        throw new NotImplementedException();
    }
}