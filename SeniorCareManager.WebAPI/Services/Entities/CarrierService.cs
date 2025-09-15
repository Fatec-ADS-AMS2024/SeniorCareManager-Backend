using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
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
            var errors = new List<FieldError>();
            var carrier = await _carrierRepository.GetById(id);

            if (carrier is null)
                throw new KeyNotFoundException("Transportadora com o id " + id + " informado não foi encontrado."); 
            return _mapper.Map<CarrierDTO>(carrier);
        }

        public override async Task Create(CarrierDTO carrierDto)
        {
            var errors = new List<FieldError>();

            if (carrierDto is null)
                throw new ArgumentNullException("A Transportadora não pode ser nula.");

            if (await CheckDuplicates(carrierDto.CpfCnpj))
                throw new InvalidOperationException("CPF/CNPJ duplicado.");
            await base.Create(carrierDto);
        }

        public override async Task Update(CarrierDTO carrierDto, int id)
        {
            var errors = new List<FieldError>();

            if (carrierDto is null)
                throw new ArgumentNullException("A Transportadora não pode ser nula.");

            var existingCarrier = await _carrierRepository.GetById(id);
            if (existingCarrier is null)
                throw new KeyNotFoundException($"Transportadora com o id {id} não foi encontrada para atualização."); // Mudança para KeyNotFoundException

            //A criação do método ExistisByCpfCnpjAsync foi necessária pois o CheckDuplicates não atende a necessidade 
            //da classe carrier, que quando era atualizada o CheckDuplicates barrava a operação devido a existência do mesmo cnpj na base de dados (a própria empresa)
            if (await _carrierRepository.ExistsByCpfCnpjAsync(carrierDto.CpfCnpj, id))
                throw new InvalidOperationException("O CNPJ informado já pertence a outra transportadora.");

            // 1. Mapeia as propriedades do DTO para a entidade que JÁ EXISTE no banco.
            //    Isso atualiza os campos de 'existingCarrier' sem tocar no Id.
            _mapper.Map(carrierDto, existingCarrier);
            await _carrierRepository.Update(existingCarrier);
        }

        public override async Task Remove(int id)
        {
            var carrier = await _carrierRepository.GetById(id);
            if (carrier is null)
                throw new KeyNotFoundException("Transportadora com o id " + id + " informado não foi encontrado."); // Mudança para KeyNotFoundException
            await base.Remove(id);
        }

        public async Task<bool> CheckDuplicates(string cpfCnpj)
        {
            var carrier = await _carrierRepository.Get();
            return carrier.Any(r => StringUtils.CompareString(r.CpfCnpj, cpfCnpj)
            );
        }
    }
}