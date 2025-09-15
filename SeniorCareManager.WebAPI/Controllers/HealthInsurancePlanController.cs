using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthInsurancePlanController : Controller
{
    private readonly IHealthInsurancePlanService _healthInsurancePlanService;

    public HealthInsurancePlanController(IHealthInsurancePlanService service)
    {
        this._healthInsurancePlanService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var healthInsurancePlan = await _healthInsurancePlanService.GetAll();
        return Response<IEnumerable<HealthInsurancePlanDTO>>.Ok(healthInsurancePlan, "Lista de plano de saúde obtidos com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var healthInsurancePlan = await _healthInsurancePlanService.GetById(id);

        return Response<HealthInsurancePlanDTO>.Ok(healthInsurancePlan, "Plano de saúde obtido com sucesso!");

    }

    [HttpPost]
    public async Task<IActionResult> Post(HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        Execute.Executar(healthInsurancePlanDto);
        healthInsurancePlanDto.Id = 0;
        await _healthInsurancePlanService.Create(healthInsurancePlanDto);

        return Response<HealthInsurancePlanDTO>.Created(healthInsurancePlanDto, "Plano de saúde Cadastrado com sucesso!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        Execute.Executar(healthInsurancePlanDto);
        await _healthInsurancePlanService.Update(healthInsurancePlanDto, id); 

        return Response<HealthInsurancePlanDTO>.Ok(healthInsurancePlanDto, "Plano de saúde atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _healthInsurancePlanService.Remove(id);

        return Response<object>.NoContent("");

    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        Execute.Executar(healthInsurancePlanDto);
        await _healthInsurancePlanService.Update(healthInsurancePlanDto, id);

        return Response<HealthInsurancePlanDTO>.Ok(healthInsurancePlanDto, "Plano de saúde atualizado com sucesso!");
    }
}

