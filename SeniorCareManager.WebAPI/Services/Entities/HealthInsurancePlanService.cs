using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class HealthInsurancePlanService : GenericService<HealthInsurancePlan, HealthInsurancePlanDTO>, IHealthInsurancePlanService
{
    private readonly IHealthInsurancePlanRepository _healthInsurancePlanRepository;
    private readonly IMapper _mapper;

    public HealthInsurancePlanService(IHealthInsurancePlanRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _healthInsurancePlanRepository = repository;
        _mapper = mapper;
    }
    public override async Task<HealthInsurancePlanDTO> GetById(int id)
    {
        var errors = new List<FieldError>();
        var healthInsurancePlan = await _healthInsurancePlanRepository.GetById(id);
        if (healthInsurancePlan is null)
            throw new ExceptionBadRequest("Plano de saúde com o id " + id + " informado não foi encontrada.");

        return _mapper.Map<HealthInsurancePlanDTO>(healthInsurancePlan);
    }
    public override async Task Create(HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        var errors = new List<FieldError>();
        if (healthInsurancePlanDto is null)
            throw new ExceptionBadRequest("O Plano de Saúde não pode ser nulo.");

        if (await CheckDuplicates(healthInsurancePlanDto))
            throw new ExceptionConflict("Nome duplicado.");

        await base.Create(healthInsurancePlanDto);
    }
    public override async Task Update(HealthInsurancePlanDTO healthInsurancePlanDto, int id)
    {
        var errors = new List<FieldError>();
        if (healthInsurancePlanDto is null)
            throw new ExceptionBadRequest("O Plano de Saúde não pode ser nulo.");

        if (await CheckDuplicates(healthInsurancePlanDto))
            throw new ExceptionConflict("Nome duplicado.");

        await base.Update(healthInsurancePlanDto, id);
    }
    public override async Task Remove(int id)
    {
        var errors = new List<FieldError>();
        var healthInsurancePlan = await _healthInsurancePlanRepository.GetById(id);
        if (healthInsurancePlan is null)
            throw new ExceptionBadRequest("Plano de saúde com o id " + id + " informado não foi encontrada.");

        await base.Remove(id);
    }
    public async Task<bool> CheckDuplicates(HealthInsurancePlanDTO dto)
    {
        var plans = await _healthInsurancePlanRepository.Get();
        return plans.Any(p =>(p.Id != dto.Id) &&(StringUtils.CompareString(p.Name, dto.Name)));
    }

}
