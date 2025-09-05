using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class CarrierService : GenericService<Carrier, CarrierDTO>, ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;
        public CarrierService(ICarrierRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _carrierRepository = repository;
            _mapper = mapper;
        }

        public override async Task<CarrierDTO> GetById(int id)
        {
            var carrier = await _carrierRepository.GetById(id);
            if (carrier is null)
                throw new ArgumentNullException("Transportadora com o id " + id + " informado não foi encontrado.");
            return _mapper.Map<CarrierDTO>(carrier);
        }

        public override async Task Create(CarrierDTO carrierDto)
        {
            if (carrierDto is null)
                throw new ArgumentNullException("A Transportadora não pode ser nula.");
            
            if (await CheckDuplicates(carrierDto.CpfCnpj))
                throw new InvalidOperationException("CPF/CNPJ duplicado.");
            await base.Create(carrierDto);
        }

        public override async Task Update(CarrierDTO carrierDto, int id)
        {
            if (carrierDto is null)
                throw new ArgumentNullException("A Transportadora não pode ser nula.");
            
            if (await CheckDuplicates(carrierDto.CpfCnpj))
                throw new InvalidOperationException("CPF/CNPJ duplicado.");
            await base.Update(carrierDto, id);
        }

        public override async Task Remove(int id)
        {
            var carrier = await _carrierRepository.GetById(id);           
            if (carrier is null)
                throw new ArgumentNullException("Transportadora com o id " + id + " informado não foi encontrado.");
            await base.Remove(id);
        }

        private async Task<bool> CheckDuplicates(string cpfCnpj)
        {
            var carrier = await _carrierRepository.Get();
            return carrier.Any(r => StringUtils.CompareString(r.CpfCnpj, cpfCnpj)
            );
        }
    }
}
