using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
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

        public override async Task<SupplierDTO> GetById(int id)
        {
            var supplier = await _supplierRepository.GetById(id);
            if (supplier is null)
                throw new ExceptionBadRequest("Fornecedor com o id " + id + " informado não foi encontrada.");

            return _mapper.Map<SupplierDTO>(supplier);
        }
        public override async Task Create(SupplierDTO supplierDto)
        {

            if (supplierDto is null)
                throw new ExceptionBadRequest("O Fornecedor não pode ser nulo.");

            if (await CheckDuplicates(supplierDto.CorporateName))
                throw new ExceptionConflict("Nome já existente.");


            await base.Create(supplierDto);
        }
        public override async Task Update(SupplierDTO supplierDto, int id)
        {
            if (supplierDto is null)
                throw new ExceptionBadRequest("O Fornecedor não pode ser nulo.");

            if (supplierDto.Id != id)
                throw new ExceptionBadRequest("O id do Fornecedor dever ser o mesmo.");

            if (await CheckDuplicates(supplierDto.CorporateName))
                throw new ExceptionConflict("Nome já existente.");

            await base.Update(supplierDto, id);
        }
        public override async Task Remove(int id)
        {
            var supplier = await _supplierRepository.GetById(id);
            if (supplier is null)
                throw new ExceptionBadRequest("Fornecedor com o id " + id + " informado não foi encontrado.");

            await base.Remove(id);
        }
        public async Task<bool> CheckDuplicates(string nome)
        {
            var suppliers = await _supplierRepository.Get();
            return suppliers.Any(r => StringUtils.CompareString(r.CorporateName, nome));
        }
    }
}