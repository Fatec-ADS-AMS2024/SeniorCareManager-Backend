using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories;

public class ProductGroupRepository : GenericRepository<ProductGroup>,  IProductGroupRepository
{
    private readonly AppDbContext _context;

    public ProductGroupRepository(AppDbContext context) : base(context)
    {
        this._context = context;
    }
    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.ProductGroups.AnyAsync(pg => pg.Id == id);
    }
}