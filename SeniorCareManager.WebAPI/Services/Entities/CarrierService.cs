using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class CarrierService : GenericService<Carrier,CarrierDTO>, ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CarrierService(ICarrierRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _carrierRepository = repository;
            _mapper = mapper;
            _context = context;
        }

        public override async Task<CarrierDTO> GetById(int id)
        {
            var carrier = await _carrierRepository.GetById(id);

            if (carrier is null)
                throw new ExceptionNotFound($"Transportadora com o id {id} não foi encontrada.");

            return _mapper.Map<CarrierDTO>(carrier);
        }

        public override async Task<CarrierDTO> Create(CarrierDTO carrierDto)
        {
            if (carrierDto is null)
                throw new ExceptionBadRequest("A transportadora não pode ser nula.");

            await ValidateCarrierBusinessRules(carrierDto);

            return await base.Create(carrierDto);
        }

        public override async Task Update(CarrierDTO carrierDto, int id)
        {
            if (carrierDto is null)
                throw new ExceptionBadRequest("A transportadora não pode ser nula.");

            if (carrierDto.Id != id)
                throw new ExceptionBadRequest("O id informado deve ser o mesmo id da transportadora.");

            var existingCarrier = await _carrierRepository.GetById(id);

            if (existingCarrier is null)
                throw new ExceptionNotFound($"Transportadora com o id {id} não foi encontrada.");

            await ValidateCarrierBusinessRules(carrierDto, id);

            await base.Update(carrierDto, id);
        }

        public override async Task Remove(int id)
        {
            var carrier = await _carrierRepository.GetById(id);

            if (carrier is null)
                throw new ExceptionNotFound($"Transportadora com o id {id} não foi encontrada.");

            await base.Remove(id);
        }

        private async Task ValidateCarrierBusinessRules(CarrierDTO carrierDto, int idToIgnore = 0)
        {
            var errors = new List<FieldError>();

            if (await ExistsByCorporateNameAsync(carrierDto.CorporateName, idToIgnore))
                errors.Add(new FieldError { Field = "corporateName", Message = "Já existe uma transportadora com esta razão social." });

            if (await ExistsByTradeNameAsync(carrierDto.TradeName, idToIgnore))
                errors.Add(new FieldError { Field = "tradeName", Message = "Já existe uma transportadora com este nome fantasia." });

            if (await ExistsByCpfCnpjAsync(carrierDto.CpfCnpj, idToIgnore))
                errors.Add(new FieldError { Field = "cpfCnpj", Message = "Já existe uma transportadora com este CPF/CNPJ." });

            if (await ExistsByEmailAsync(carrierDto.Email, idToIgnore))
                errors.Add(new FieldError { Field = "email", Message = "Já existe uma transportadora com este e-mail." });

            if (await ExistsByPhoneAsync(carrierDto.Phone, idToIgnore))
                errors.Add(new FieldError { Field = "phone", Message = "Já existe uma transportadora com este telefone." });

            if (errors.Count > 0)
                throw new ExceptionConflict("Não foi possível concluir a operação devido a dados duplicados.", errors);
        }

        private IQueryable<Carrier> QueryCarriers()
        {
            return _context.Carriers.AsNoTracking();
        }

        private async Task<bool> ExistsByCorporateNameAsync(string? corporateName, int idToIgnore)
        {
            if (string.IsNullOrWhiteSpace(corporateName))
                return false;

            var normalized = NormalizeText(corporateName);
            return await QueryCarriers()
                .AnyAsync(c => c.Id != idToIgnore && c.CorporateName.ToLower() == normalized);
        }

        private async Task<bool> ExistsByTradeNameAsync(string? tradeName, int idToIgnore)
        {
            if (string.IsNullOrWhiteSpace(tradeName))
                return false;

            var normalized = NormalizeText(tradeName);
            return await QueryCarriers()
                .AnyAsync(c => c.Id != idToIgnore && c.TradeName.ToLower() == normalized);
        }

        private async Task<bool> ExistsByCpfCnpjAsync(string? cpfCnpj, int idToIgnore)
        {
            var normalized = NormalizeDigits(cpfCnpj);

            if (string.IsNullOrWhiteSpace(normalized))
                return false;

            return await QueryCarriers()
                .AnyAsync(c => c.Id != idToIgnore && c.CpfCnpj == normalized);
        }

        private async Task<bool> ExistsByEmailAsync(string? email, int idToIgnore)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var normalized = NormalizeText(email);
            return await QueryCarriers()
                .AnyAsync(c => c.Id != idToIgnore && c.Email.ToLower() == normalized);
        }

        private async Task<bool> ExistsByPhoneAsync(string? phone, int idToIgnore)
        {
            var normalized = NormalizeDigits(phone);

            if (string.IsNullOrWhiteSpace(normalized))
                return false;

            return await QueryCarriers()
                .AnyAsync(c => c.Id != idToIgnore && c.Phone == normalized);
        }

        private static string NormalizeText(string value)
        {
            return value.Trim().ToLower();
        }

        private static string NormalizeDigits(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            return new string(value.Where(char.IsDigit).ToArray());
        }
    }
}
