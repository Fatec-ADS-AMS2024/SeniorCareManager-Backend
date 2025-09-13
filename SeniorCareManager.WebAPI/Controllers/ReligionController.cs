using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class ReligionController : Controller
{
    private readonly IReligionService _religionService;

    public ReligionController(IReligionService service)
    {
        this._religionService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var religions = await _religionService.GetAll();
        return Response<object>.Ok(religions, "Lista de religiões!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var religion = await _religionService.GetById(id);
        return Response<object>.Ok(religion, "Religião encontrada!");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ReligionDTO religionDto)
    {
        religionDto.id = 0;
        await _religionService.Create(religionDto);
        return Response<object>.Created(religionDto, "Religião cadastrada com sucesso!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ReligionDTO religionDto)
    {
        await _religionService.Update(religionDto, id);
        return Response<object>.Ok(religionDto, "Religião alterada com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _religionService.Remove(id);
        return Response<object>.NoContent("A religião apagada com sucesso!");
    }
}