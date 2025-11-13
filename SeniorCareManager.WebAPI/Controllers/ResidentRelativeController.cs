using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;


namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class ResidentRelativeController : Controller
{
    private readonly IResidentRelativeService _residentRelativeService;
    public ResidentRelativeController(IResidentRelativeService service)
    {
        this._residentRelativeService = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> Get()
    {
        var residentRelatives = await _residentRelativeService.GetAll();
        return Response<IEnumerable<ResidentRelativeDTO>>.Ok(residentRelatives, "Lista de Parentes obtidas com sucesso!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {
        var residentRelative = await _residentRelativeService.GetById(id);
        return Response<ResidentRelativeDTO>.Ok(residentRelative, "Parente do Residente obtido com sucesso!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(ResidentRelativeDTO residentRelativeDto)
    {
        Execute.Executar(residentRelativeDto);
        residentRelativeDto.Id = 0;
        var created = await _residentRelativeService.Create(residentRelativeDto);
        return Response<ResidentRelativeDTO>.Created(created, "Parentes do Residente cadastrado com sucesso!");
    }

    [HttpPut, MapToApiVersion("1")]
    public async Task<IActionResult> Put(ResidentRelativeDTO residentRelativeDto)
    {
        Execute.Executar(residentRelativeDto);
        // Update receberá o id via corpo (residentRelativeDto.Id)
        await _residentRelativeService.Update(residentRelativeDto, residentRelativeDto.Id);
        return Response<ResidentRelativeDTO>.Ok(residentRelativeDto, "Parentes do Residente atualizado com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {
        await _residentRelativeService.Remove(id);
        return Response<object>.NoContent();
    }
}
