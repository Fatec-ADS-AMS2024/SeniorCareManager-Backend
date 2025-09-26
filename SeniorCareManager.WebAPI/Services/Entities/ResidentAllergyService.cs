using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ResidentAllergyService : GenericService<ResidentAllergy, ResidentAllergyDTO>, IResidentAllergyService
{
    private readonly IResidentAllergyRepository _residentAllergyRepository;
    private readonly IMapper _mapper;

    public ResidentAllergyService(IResidentAllergyRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _residentAllergyRepository = repository;
        _mapper = mapper;
    }

    public override async Task<ResidentAllergyDTO> GetById(int id)
    {
        var allergy = await _residentAllergyRepository.GetById(id);
        if (allergy is null)
            throw new ExceptionBadRequest("Alergia do residente com o id " + id + " informado não foi encontrada.");

        return _mapper.Map<ResidentAllergyDTO>(allergy);
    }

    public override async Task Create(ResidentAllergyDTO residentAllergyDto)
    {
        if (residentAllergyDto is null)
            throw new ExceptionBadRequest("A alergia do residente não pode ser nula.");

        if (await CheckDuplicates(residentAllergyDto.ResidentId, residentAllergyDto.Description))
            throw new ExceptionConflict("Alergia já cadastrada para este residente.");

        await base.Create(residentAllergyDto);
    }

    public override async Task Update(ResidentAllergyDTO residentAllergyDto, int id)
    {
        if (residentAllergyDto is null)
            throw new ExceptionBadRequest("A alergia do residente não pode ser nula.");

        if (residentAllergyDto.Id != id)
            throw new ExceptionBadRequest("O id da alergia do residente deve ser o mesmo.");

        if (await CheckDuplicates(residentAllergyDto.ResidentId, residentAllergyDto.Description))
            throw new ExceptionConflict("Alergia já cadastrada para este residente.");

        await base.Update(residentAllergyDto, id);
    }

    public override async Task Remove(int id)
    {
        var allergy = await _residentAllergyRepository.GetById(id);
        if (allergy is null)
            throw new ExceptionBadRequest("Alergia do residente com o id " + id + " informado não foi encontrada.");

        await base.Remove(id);
    }

    public async Task<bool> CheckDuplicates(int residentId, string description)
    {
        var allergies = await _residentAllergyRepository.Get();
        return allergies.Any(a =>
            a.ResidentId == residentId &&
            StringUtils.CompareString(a.Description, description)
        );
    }
}
