using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class ResidentRelativeService : GenericService<ResidentRelative, ResidentRelativeDTO>, IResidentRelativeService
    {
        private readonly IResidentRelativeRepository _residentRelativeRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public ResidentRelativeService(IResidentRelativeRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _residentRelativeRepository = repository;
            _mapper = mapper;
            _context = context;
        }

        public override async Task<ResidentRelativeDTO> GetById(int id)
        {
            var residentRelative = await _residentRelativeRepository.GetById(id);
            if (residentRelative is null)
                throw new ExceptionNotFound("Parente do residente com o id " + id + " informado não foi encontrado.");

            return _mapper.Map<ResidentRelativeDTO>(residentRelative);
        }

        public override async Task<ResidentRelativeDTO> Create(ResidentRelativeDTO residentRelativeDto)
        {
            if (residentRelativeDto is null)
                throw new ExceptionBadRequest("O Parente do Residente não pode ser nulo.");

            // Verifica se o residente existe
            var residentExists = await _context.Set<Resident>().AnyAsync(r => r.Id == residentRelativeDto.ResidentId);
            if (!residentExists)
                throw new ExceptionBadRequest("Residente com o id " + residentRelativeDto.ResidentId + " informado não foi encontrado.");

            // Verifica duplicados (mesmo nome, mesmo residente e mesmo parentesco)
            if (await CheckDuplicates(residentRelativeDto.Name, residentRelativeDto.ResidentId, residentRelativeDto.Relationship))
                throw new ExceptionConflict("Parente do residente duplicado.");

            return _mapper.Map<ResidentRelativeDTO>(await base.Create(residentRelativeDto));
        }

        public override async Task Update(ResidentRelativeDTO residentRelativeDto, int id)
        {
            if (residentRelativeDto is null)
                throw new ExceptionBadRequest("O Parente do Residente não pode ser nulo.");

            if (residentRelativeDto.Id != id)
                throw new ExceptionBadRequest("O id do Parente do Residente dever ser o mesmo.");

            // Verifica se o residente existe
            var residentExists = await _context.Set<Resident>().AnyAsync(r => r.Id == residentRelativeDto.ResidentId);
            if (!residentExists)
                throw new ExceptionBadRequest("Residente com o id " + residentRelativeDto.ResidentId + " informado não foi encontrado.");

            // Verifica duplicados ignorando o próprio registro que está sendo atualizado
            if (await CheckDuplicatesForUpdate(residentRelativeDto.Name, residentRelativeDto.ResidentId, residentRelativeDto.Relationship, id))
                throw new ExceptionConflict("Parente do residente duplicado.");

            await base.Update(residentRelativeDto, id);
        }

        public override async Task Remove(int id)
        {
            var residentRelative = await _residentRelativeRepository.GetById(id);
            if (residentRelative is null)
                throw new ExceptionNotFound("Parente do residente com o id " + id + " informado não foi encontrado.");

            await base.Remove(id);
        }

        public async Task<bool> CheckDuplicates(string name, int residentId, Relationship relationship)
        {
            var relatives = await _residentRelativeRepository.Get();
            return relatives.Any(r =>
                r.ResidentId == residentId &&
                r.Relationship == relationship &&
                StringUtils.CompareString(r.Name, name)
            );
        }

        public async Task<bool> CheckDuplicatesForUpdate(string name, int residentId, Relationship relationship, int id)
        {
            var relatives = await _residentRelativeRepository.Get();
            return relatives.Any(r =>
                r.Id != id &&
                r.ResidentId == residentId &&
                r.Relationship == relationship &&
                StringUtils.CompareString(r.Name, name)
            );
        }
    }
}
