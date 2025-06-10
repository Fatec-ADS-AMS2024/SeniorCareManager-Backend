using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class SupplierService : GenericService<Supplier, SupplierDTO>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _supplierRepository = repository;
            _mapper = mapper;
        }

        public async Task<bool> ExistsByCpfCnpj(string cpfCnpj, int? excludeId = null)
        {
            cpfCnpj = StringValidator.ExtractNumbers(cpfCnpj);

            var suppliers = await _supplierRepository.Get();

            return suppliers.Any(s =>
                StringValidator.ExtractNumbers(s.CpfCnpj) == cpfCnpj &&
                (!excludeId.HasValue || s.Id != excludeId.Value));
        }

        public async Task<bool> ExistsByCorporateName(string corporateName, int? excludeId = null)
        {
            var suppliers = await _supplierRepository.Get();

            return suppliers.Any(s =>
                StringValidator.CompareString(s.CorporateName, corporateName) &&
                (!excludeId.HasValue || s.Id != excludeId.Value));
        }
    }
}
