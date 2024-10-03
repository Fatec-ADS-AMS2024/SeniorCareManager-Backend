﻿using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories;
public class ProductTypeRepository : GenericRepository<ProductType>, IProductTypeRepository
{
    private readonly AppDbContext _context;
    
    public ProductTypeRepository(AppDbContext context): base(context)
    {
        this._context = context;
    }
}

