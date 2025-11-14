using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class ReligionController : Controller
{
    private readonly IReligionService _religionService;

    public ReligionController(IReligionService service)
    {
        this._religionService = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> Get()
    {
        var religions = await _religionService.GetAll();
        return Response<object>.Ok(religions, "Lista de religiões!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {

        var religion = await _religionService.GetById(id);
        return Response<object>.Ok(religion, "Religião encontrada!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post([FromBody] ReligionDTO religionDto)
    {
        Execute.Executar(religionDto);
        religionDto.id = 0;
        return Response<object>.Created(await _religionService.Create(religionDto), "Religião cadastrada com sucesso!");
    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(int id, [FromBody] ReligionDTO religionDto)
    {
        Execute.Executar(religionDto);
        await _religionService.Update(religionDto, id);
        return Response<object>.Ok(religionDto, "Religião alterada com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {
        await _religionService.Remove(id);

        return Response<object>.NoContent();
    }
}
