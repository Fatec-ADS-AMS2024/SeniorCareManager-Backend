using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ResidentController : Controller
{
    private readonly IResidentService _residentService;

    public ResidentController(IResidentService residentService)
    {
        _residentService = residentService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var residents = await _residentService.GetAll();
        return Response<IEnumerable<ResidentDTO>>.Ok(residents, "Lista de Resident obtida com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resident = await _residentService.GetById(id);
        return Response<ResidentDTO>.Ok(resident, "Residente obtido com sucesso!");
    }

    [HttpPost]
    public async Task<IActionResult> Post(ResidentDTO residentDto)
    {
        residentDto.DateOfBirth = DateTime.SpecifyKind(residentDto.DateOfBirth, DateTimeKind.Utc);

        Execute.Executar(residentDto);
        residentDto.Id = 0;
        await _residentService.Create(residentDto);

        return Response<ResidentDTO>.Created(residentDto, "Residente cadastrado com sucesso!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ResidentDTO residentDto)
    {
        residentDto.DateOfBirth = DateTime.SpecifyKind(residentDto.DateOfBirth, DateTimeKind.Utc);

        Execute.Executar(residentDto);
        await _residentService.Update(residentDto, id);

        return Response<ResidentDTO>.Ok(residentDto, "Residente atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _residentService.Remove(id);
        return Response<object>.NoContent();
    }
}
