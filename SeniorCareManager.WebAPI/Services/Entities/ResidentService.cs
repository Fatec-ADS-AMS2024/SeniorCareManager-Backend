using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class ResidentService : GenericService<Resident, ResidentDTO>, IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ResidentService(IResidentRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _residentRepository = repository;
            _mapper = mapper;
            _context = context;
        }

        public override async Task<ResidentDTO> GetById(int id)
        {
            /*
             * Busca por id o registro
             * Caso não for encontrado retorna NotFound 
             */
            var resident = await _residentRepository.GetById(id);
            if (resident is null)
                throw new ExceptionNotFound("Residente com o id " + id + " informado não foi encontrado.");
            return _mapper.Map<ResidentDTO>(resident);
        }

        public override async Task<ResidentDTO> Create(ResidentDTO residentDto)
        {
            /*
             * Verifica se não é nulo e CPF duplicado
             */
            if (residentDto is null)
                throw new ExceptionBadRequest("O Residente não pode ser nulo.");

            if (await CheckDuplicates(residentDto.Cpf))
                throw new ExceptionConflict("CPF duplicado.");

            return _mapper.Map<ResidentDTO>(await base.Create(residentDto));
        }

        public override async Task Update(ResidentDTO residentDto, int id)
        {
            /*
             * Atualiza um residente
             * Verifica se o id inserido está correto e CPF duplicado
             * Caso der erros retorna lista de erros
             */
            var errors = new List<FieldError>();
            if (residentDto is null)
                throw new ExceptionBadRequest("O Residente não pode ser nulo.");

            if (residentDto.Id != id)
                throw new ExceptionBadRequest("O id do Residente dever ser o mesmo.");

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
            /*
             * Remove o residente
             * Se estiver sendo utilizado em parentes ou alergias não apaga
             */
            var resident = await _residentRepository.GetById(id);
            var isInUse = await _context.Set<ResidentRelative>().AnyAsync(rr => rr.ResidentId == id)
                        || await _context.Set<ResidentAllergy>().AnyAsync(ra => ra.ResidentId == id);

            if (isInUse)
                throw new InvalidOperationException("Esse residente não pode ser removido pois está vinculado a um ou mais registros.");

            if (resident is null)
                throw new ExceptionNotFound("Residente com o id " + id + " informado não foi encontrado.");

            await base.Remove(id);
        }
    }
}
