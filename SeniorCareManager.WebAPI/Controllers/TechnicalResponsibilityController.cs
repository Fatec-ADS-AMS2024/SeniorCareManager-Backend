using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using Microsoft.AspNetCore.Authorization;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
// [Authorize]
public class TechnicalResponsibilityController : Controller
{
    private readonly ITechnicalResponsibilityService _technicalResponsibilityService;

    public TechnicalResponsibilityController(ITechnicalResponsibilityService service)
    {
        this._technicalResponsibilityService = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> Get()
    {
        var technicalResponsibilities = await _technicalResponsibilityService.GetAll();
        return Response<IEnumerable<TechnicalResponsibilityDTO>>.Ok(technicalResponsibilities, "Lista de Responsabilidades Técnicas obtidas com sucesso!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {
        var positechnicalResponsibility = await _technicalResponsibilityService.GetById(id);

        return Response<TechnicalResponsibilityDTO>.Ok(positechnicalResponsibility, "Responsabilidade Técnica obtido com sucesso!");

    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(TechnicalResponsibilityDTO technicalResponsibilityDto)
    {
        Execute.Executar(technicalResponsibilityDto);
        technicalResponsibilityDto.Id = 0;
        await _technicalResponsibilityService.Create(technicalResponsibilityDto);

        return Response<TechnicalResponsibilityDTO>.Created(technicalResponsibilityDto, "Responsabilidade Técnica Cadastrado com sucesso!");

    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(int id, TechnicalResponsibilityDTO technicalResponsibilityDto)
    {
        Execute.Executar(technicalResponsibilityDto);
        await _technicalResponsibilityService.Update(technicalResponsibilityDto, id); ;

        return Response<TechnicalResponsibilityDTO>.Ok(technicalResponsibilityDto, "Responsabilidade Técnica atualizado com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {

        await _technicalResponsibilityService.Remove(id);

        return Response<object>.NoContent();
    }
}
