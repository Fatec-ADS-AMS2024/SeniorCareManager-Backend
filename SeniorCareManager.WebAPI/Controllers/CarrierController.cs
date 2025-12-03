using Microsoft.AspNetCore.Authorization;
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
public class CarrierController : Controller
{

    private readonly ICarrierService _carrierService;

    public CarrierController(ICarrierService carrierService)
    {
        this._carrierService = carrierService;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> GetAll()
    {
        var carriers = await _carrierService.GetAll();
        return Response<IEnumerable<CarrierDTO>>.Ok(carriers, "Lista de transportadoras obtida com sucesso!");

    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {
        var carrier = await _carrierService.GetById(id);
        return Response<CarrierDTO>.Ok(carrier, "Transportadora obtida com sucesso!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(CarrierDTO carrierDto)
    {
        Execute.Executar(carrierDto);
        carrierDto.Id = 0;

        return Response<CarrierDTO>.Created(await _carrierService.Create(carrierDto), "Transportadora cadastrada com sucesso!");
    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(int id, CarrierDTO carrierDto)
    {
        Execute.Executar(carrierDto);
        await _carrierService.Update(carrierDto, id);
        return Response<CarrierDTO>.Ok(carrierDto, "Transportadora atualizada com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {
        await _carrierService.Remove(id);
        return Response<object>.NoContent();
    }
}
