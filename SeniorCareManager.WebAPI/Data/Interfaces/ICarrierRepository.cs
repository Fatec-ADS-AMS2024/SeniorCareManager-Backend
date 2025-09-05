using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces
{
    public interface ICarrierRepository : IGenericRepository<Carrier>
    {
        Task<bool> ExistsByCpfCnpjAsync(string cpfCnpj, int currentId);
    }
}
