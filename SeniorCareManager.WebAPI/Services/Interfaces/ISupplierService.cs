using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Models;
using System.Threading;

namespace SeniorCareManager.WebAPI.Services.Interfaces;

public interface ISupplierService : IGenericService<Supplier, SupplierDTO>
{
    public interface ISupplierService : IGenericService<Supplier, SupplierDTO>
    {
        Task<bool> CheckDuplicates(Func<Supplier, string?> selector, string? valor, int idIgnor);
    }
}