using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TechnicalResponsibilityController : Controller
{
    private readonly ITechnicalResponsibilityService _technicalResponsibilityService;

    public TechnicalResponsibilityController(ITechnicalResponsibilityService service)
    {
        this._technicalResponsibilityService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var technicalResponsibilities = await _technicalResponsibilityService.GetAll();
        return Response<IEnumerable<TechnicalResponsibilityDTO>>.Ok(technicalResponsibilities, "Lista de Position obtidas com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var positechnicalResponsibility = await _technicalResponsibilityService.GetById(id);

        return Response<TechnicalResponsibilityDTO>.Ok(positechnicalResponsibility, "Position obtido com sucesso!");

    }

    [HttpPost]
    public async Task<IActionResult> Post(TechnicalResponsibilityDTO technicalResponsibilityDto)
    {
        Execute.Executar(technicalResponsibilityDto);
        technicalResponsibilityDto.Id = 0;
        await _technicalResponsibilityService.Create(technicalResponsibilityDto);

        return Response<TechnicalResponsibilityDTO>.Created(technicalResponsibilityDto, "Cargo Cadastrado com sucesso!");

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TechnicalResponsibilityDTO technicalResponsibilityDto)
    {
        Execute.Executar(technicalResponsibilityDto);
        await _technicalResponsibilityService.Update(technicalResponsibilityDto, id); ;

        return Response<TechnicalResponsibilityDTO>.Ok(technicalResponsibilityDto, "Cargo atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        await _technicalResponsibilityService.Remove(id);

        return Response<object>.NoContent();
    }
}