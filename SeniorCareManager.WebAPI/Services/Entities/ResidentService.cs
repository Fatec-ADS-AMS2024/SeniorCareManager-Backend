using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class ResidentService : GenericService<Resident, ResidentDTO>, IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IMapper _mapper;
        public ResidentService(IResidentRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _residentRepository = repository;
            _mapper = mapper;
        }
        public override async Task<ResidentDTO> GetById(int id)
        {
            var resident = await _residentRepository.GetById(id);
            if (resident is null)
                throw new ExceptionBadRequest("Residente com o id " + id + " informado não foi encontrado.");
            return _mapper.Map<ResidentDTO>(resident);
        }
        public override async Task Create(ResidentDTO residentDto)
        {
            if (residentDto is null)
                throw new ExceptionBadRequest("O Residente não pode ser nulo.");

            if (await CheckDuplicates(residentDto.Cpf))
                throw new ExceptionConflict("CPF duplicado.");

            await base.Create(residentDto);
        }
        public override async Task Update(ResidentDTO residentDto, int id)
        {
            var errors = new List<FieldError>();
            if (residentDto is null)
                throw new ExceptionBadRequest("O Residente não pode ser nulo.");

            if (await CheckDuplicates(residentDto.Cpf, id))
                errors.Add(new FieldError { Field = "CPF", Message = "CPF duplicado." });

            if (errors.Count() > 0)
                throw new ExceptionBadRequest("Erros na requisição", errors);

            residentDto.Id = id;

            await base.Update(residentDto, id);
        }
        private static string NormalizeCpfCnpj(string value)
            => new string((value ?? string.Empty).Where(char.IsDigit).ToArray());

        private async Task<bool> CheckDuplicates(string cpf, int? ignoreId = null)
        {
            var normalized = NormalizeCpfCnpj(cpf);

            var residents = await _residentRepository.Get();
            var resident = residents.FirstOrDefault(r => NormalizeCpfCnpj(r.Cpf) == normalized);

            if (resident is null) return false;
            return !ignoreId.HasValue || resident.Id != ignoreId.Value;
        }
        public override async Task Remove(int id)
        {
            var resident = await _residentRepository.GetById(id);
            if (resident is null)
                throw new ExceptionConflict("Residente com o id " + id + " informado não foi encontrado.");
            await base.Remove(id);
        }
    }
}
