using SeniorCareManager.WebAPI.Objects.Models;
using System.Threading;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Dtos;

namespace SeniorCareManager.WebAPI.Services.Interfaces
{
    public interface ISupplierService : IGenericService<Supplier, SupplierDTO>
    {
    }
}