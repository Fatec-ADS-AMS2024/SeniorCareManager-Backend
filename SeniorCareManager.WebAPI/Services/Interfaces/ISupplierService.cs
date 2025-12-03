using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Models;
using System.Linq.Expressions;

namespace SeniorCareManager.WebAPI.Services.Interfaces;


public interface ISupplierService : IGenericService<Supplier, SupplierDTO>
{
    Task<bool> CheckDuplicates(Expression<Func<Supplier, string?>> selector, string? valor, int idIgnor);
}
