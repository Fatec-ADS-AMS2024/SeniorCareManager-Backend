using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
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
    public override async Task<HealthInsurancePlanDTO> Create(HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        var errors = new List<FieldError>();
     
        if (healthInsurancePlanDto is null)
            throw new ExceptionBadRequest("O Plano de Saúde não pode ser nulo.");

        if (await CheckDuplicates(p => p.Name, healthInsurancePlanDto.Name, healthInsurancePlanDto.Id))
            throw new ExceptionConflict("Nome duplicado.");
       

        return _mapper.Map<HealthInsurancePlanDTO>( await base.Create(healthInsurancePlanDto) );
    }
    public override async Task Update(HealthInsurancePlanDTO healthInsurancePlanDto, int id)
    {
        var errors = new List<FieldError>();
        if (healthInsurancePlanDto is null)
            throw new ExceptionBadRequest("O Plano de Saúde não pode ser nulo.");

        if (healthInsurancePlanDto.Id != id)
            throw new ExceptionBadRequest("O id de Plano de Saúde dever ser o mesmo.");

        if (await CheckDuplicates(p => p.Name, healthInsurancePlanDto.Name, healthInsurancePlanDto.Id))
            errors.Add(new FieldError { Field = "Nome", Message = "Nome duplicado." });

        if (errors.Count() > 0)
            throw new ExceptionBadRequest("Erros na requisição", errors);

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
    public async Task<bool> CheckDuplicates(Func<HealthInsurancePlan, string?> selector, string? valor, int idIgnor)
    {
        var planos = await _healthInsurancePlanRepository.Get();
        return planos.Any(p =>
            p.Id != idIgnor &&
            StringUtils.CompareString(selector(p)!, valor)
        );
    }

}
