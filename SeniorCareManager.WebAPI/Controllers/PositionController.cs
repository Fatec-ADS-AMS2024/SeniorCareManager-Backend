using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PositionController : Controller
{
    private readonly IPositionService _positionService;

    public PositionController(IPositionService service)
    {
        this._positionService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var positions = await _positionService.GetAll();
        return Response<IEnumerable<PositionDTO>>.Ok(positions, "Lista de Position obtidas com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var position = await _positionService.GetById(id);
        
        return Response<PositionDTO>.Ok(position, "Position obtido com sucesso!");
    
    }

    [HttpPost]
    public async Task<IActionResult> Post(PositionDTO positionDto)
    {
        Execute.Executar(positionDto);
        positionDto.Id = 0;
        await _positionService.Create(positionDto);

        return Response<PositionDTO>.Created(positionDto, "Cargo Cadastrado com sucesso!"); 

    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Put(int id, PositionDTO positionDto)
    {
        Execute.Executar(positionDto);
        await _positionService.Update(positionDto, id); ;

        return Response<PositionDTO>.Ok(positionDto, "Cargo atualizado com sucesso!"); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        await _positionService.Remove(id);

        return Response<object>.NoContent("Grupo de cargo apagado com sucesso!");
    }
}