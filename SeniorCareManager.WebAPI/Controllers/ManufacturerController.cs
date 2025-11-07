using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class ManufacturerController : Controller
{
    private readonly IManufacturerService _manufacturerService;

    public ManufacturerController(IManufacturerService service)
    {
        _manufacturerService = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> Get()
    {
        var manufacturers = await _manufacturerService.GetAll();
        return Response<IEnumerable<ManufacturerDTO>>.Ok(manufacturers, "Lista de fabricantes obtida com sucesso!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {
        var manufacturer = await _manufacturerService.GetById(id);
        return Response<ManufacturerDTO>.Ok(manufacturer, "Fabricante obtido com sucesso!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(ManufacturerDTO manufacturerDto)
    {
        Execute.Executar(manufacturerDto);
        manufacturerDto.Id = 0;

        return Response<ManufacturerDTO>.Created(await _manufacturerService.Create(manufacturerDto), "Fabricante cadastrado com sucesso!");
    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(int id, ManufacturerDTO manufacturerDto)
    {
        Execute.Executar(manufacturerDto);
        await _manufacturerService.Update(manufacturerDto, id);

        return Response<ManufacturerDTO>.Ok(manufacturerDto, "Fabricante atualizado com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {
        await _manufacturerService.Remove(id);
        return Response<object>.NoContent();
    }
}