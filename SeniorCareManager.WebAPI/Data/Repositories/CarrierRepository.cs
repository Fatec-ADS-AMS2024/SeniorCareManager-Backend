using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Data.Repositories
{
    public class CarrierRepository : GenericRepository<Carrier>, ICarrierRepository
    {
        private readonly AppDbContext _context;

        public CarrierRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> ExistsByCpfCnpjAsync(string cpfCnpj, int currentId)
        {
            var cleanedCpfCnpj = StringUtils.Clean(cpfCnpj);
            // Procura por qualquer carrier com o mesmo CNPJ E um ID diferente do atual.
            return await _context.Carriers.AnyAsync(c => c.CpfCnpj == cleanedCpfCnpj && c.Id != currentId);
        }

    }
}
