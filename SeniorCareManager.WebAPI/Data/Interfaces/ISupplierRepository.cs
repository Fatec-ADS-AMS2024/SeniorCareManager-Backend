using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces;

public interface ISupplierRepository : IGenericRepository<Supplier>
{
    Task<bool> ExistsAsync(Expression<Func<Supplier, string?>> selector, string? value, int idIgnor);
}