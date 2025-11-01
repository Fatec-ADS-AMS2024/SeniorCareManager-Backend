using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using Microsoft.AspNetCore.Authorization;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class PositionController : Controller
{
    private readonly IPositionService _positionService;

    public PositionController(IPositionService service)
    {
        this._positionService = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> Get()
    {
        var positions = await _positionService.GetAll();
        return Response<IEnumerable<PositionDTO>>.Ok(positions, "Lista de Position obtidas com sucesso!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {
        var position = await _positionService.GetById(id);

        return Response<PositionDTO>.Ok(position, "Position obtido com sucesso!");

    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(PositionDTO positionDto)
    {
        Execute.Executar(positionDto);
        positionDto.Id = 0;

        return Response<PositionDTO>.Created(await _positionService.Create(positionDto), "Cargo Cadastrado com sucesso!");

    }
    public async Task<IActionResult> Post(PositionDTO position)
    {
        try
        {
            await _positionService.Create(position);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo cargo.");
        }
        return Ok(position);
    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(int id, PositionDTO positionDto)
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PositionDTO position)
    {
        Execute.Executar(positionDto);
        await _positionService.Update(positionDto, id); 

        return Response<PositionDTO>.Ok(positionDto, "Cargo atualizado com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {

        await _positionService.Remove(id);

        return Response<object>.NoContent();
    }
}