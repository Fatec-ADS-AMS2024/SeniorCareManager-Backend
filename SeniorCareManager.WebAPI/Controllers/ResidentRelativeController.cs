using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;


namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ResidentRelativeController : Controller
{
    private readonly IResidentRelativeService _residentRelativeService;
    public ResidentRelativeController(IResidentRelativeService service)
    {
        this._residentRelativeService = service;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var residentRelatives = await _residentRelativeService.GetAll();
        return Response<IEnumerable<ResidentRelativeDTO>>.Ok(residentRelatives, "Lista de ResidentRelatives obtidas com sucesso!");
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var residentRelative = await _residentRelativeService.GetById(id);
        return Response<ResidentRelativeDTO>.Ok(residentRelative, "ResidentRelative obtido com sucesso!");
    }
    [HttpPost]
    public async Task<IActionResult> Post(ResidentRelativeDTO residentRelativeDto)
    {
        Execute.Executar(residentRelativeDto);
        residentRelativeDto.Id = 0;
        await _residentRelativeService.Create(residentRelativeDto);
        return Response<ResidentRelativeDTO>.Created(residentRelativeDto, "Parente do Residente cadastrado com sucesso!");
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ResidentRelativeDTO residentRelativeDto)
    {
        Execute.Executar(residentRelativeDto);
        await _residentRelativeService.Update(residentRelativeDto, id);
        return Response<ResidentRelativeDTO>.Ok(residentRelativeDto, "Parente do Residente atualizado com sucesso!");
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
            await _residentRelativeService.Remove(id);
        return Response<string>.Ok(null, "Parente do Residente excluído com sucesso!");
    }
}
