using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ResidentAllergyService : GenericService<ResidentAllergy, ResidentAllergyDTO>, IResidentAllergyService
{
    private readonly IResidentAllergyRepository _residentAllergyRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public ResidentAllergyService(IResidentAllergyRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
    {
        _residentAllergyRepository = repository;
        _mapper = mapper;
        _context = context;
    }

    public override async Task<ResidentAllergyDTO> GetById(int id)
    {
        var allergy = await _residentAllergyRepository.GetById(id);
        if (allergy is null)
            throw new ExceptionNotFound("Alergia do residente com o id " + id + " informado não foi encontrada.");

        return _mapper.Map<ResidentAllergyDTO>(allergy);
    }

    public override async Task<ResidentAllergyDTO> Create(ResidentAllergyDTO residentAllergyDto)
    {
        if (residentAllergyDto is null)
            throw new ExceptionBadRequest("A alergia do residente não pode ser nula.");

        // valida campo obrigatório Description (coluna NOT NULL no BD)
        if (string.IsNullOrWhiteSpace(residentAllergyDto.Description))
            throw new ExceptionBadRequest("Descrição é obrigatória.");

            // verifica existência do residente e da alergia
        var residentExists = await _context.Set<Resident>().AnyAsync(r => r.Id == residentAllergyDto.ResidentId);
        if (!residentExists)
            throw new ExceptionBadRequest("Residente com o id " + residentAllergyDto.ResidentId + " informado não foi encontrado.");

        var allergyExists = await _context.Set<Allergy>().AnyAsync(a => a.Id == residentAllergyDto.AllergyId);
        if (!allergyExists)
            throw new ExceptionBadRequest("Alergia com o id " + residentAllergyDto.AllergyId + " informado não foi encontrada.");

        if (await CheckDuplicates(residentAllergyDto.ResidentId, residentAllergyDto.AllergyId))
            throw new ExceptionConflict("Alergia já cadastrada para este residente.");

        return _mapper.Map<ResidentAllergyDTO>(await base.Create(residentAllergyDto));
    }

    private async Task<bool> CheckDuplicates(int residentId, int allergyId)
    {
        var allergies = await _residentAllergyRepository.Get();
        return allergies.Any(a =>
            a.ResidentId == residentId &&
            a.AllergyId == allergyId
        );
    }

    public override async Task Update(ResidentAllergyDTO residentAllergyDto, int id)
    {
        if (residentAllergyDto is null)
            throw new ExceptionBadRequest("A alergia do residente não pode ser nula.");

        if (residentAllergyDto.Id != id)
            throw new ExceptionBadRequest("O id da alergia do residente deve ser o mesmo.");

        // valida campo obrigatório Description
        if (string.IsNullOrWhiteSpace(residentAllergyDto.Description))
            throw new ExceptionBadRequest("Descrição é obrigatória.");

        // valida existência das chaves relacionadas
        var residentExists = await _context.Set<Resident>().AnyAsync(r => r.Id == residentAllergyDto.ResidentId);
        if (!residentExists)
            throw new ExceptionBadRequest("Residente com o id " + residentAllergyDto.ResidentId + " informado não foi encontrado.");

        var allergyExists = await _context.Set<Allergy>().AnyAsync(a => a.Id == residentAllergyDto.AllergyId);
        if (!allergyExists)
            throw new ExceptionBadRequest("Alergia com o id " + residentAllergyDto.AllergyId + " informado não foi encontrada.");

        if (await CheckDuplicates(residentAllergyDto.ResidentId, residentAllergyDto.AllergyId))
            throw new ExceptionConflict("Alergia já cadastrada para este residente.");

        await base.Update(residentAllergyDto, id);
    }

    public override async Task Remove(int id)
    {
        var allergy = await _residentAllergyRepository.GetById(id);
        if (allergy is null)
            throw new ExceptionNotFound("Alergia do residente com o id " + id + " informado não foi encontrada.");

        await base.Remove(id);
    }
}
