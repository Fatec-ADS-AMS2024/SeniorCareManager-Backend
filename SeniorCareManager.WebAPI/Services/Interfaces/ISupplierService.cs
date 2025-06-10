using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Models;
using System.Threading;

namespace SeniorCareManager.WebAPI.Services.Interfaces;

public interface ISupplierService : IGenericService<Supplier, SupplierDTO>
{
    Task<bool> ExistsByCpfCnpj(string cpfCnpj, int? excludeId = null);
    Task<bool> ExistsByCorporateName(string corporateName, int? excludeId = null);
}