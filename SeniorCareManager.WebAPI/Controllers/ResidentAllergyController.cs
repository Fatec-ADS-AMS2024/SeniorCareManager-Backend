using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ResidentAllergyController : Controller
{
    private readonly IResidentAllergyService _residentAllergyService;

    public ResidentAllergyController(IResidentAllergyService service)
    {
        _residentAllergyService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allergies = await _residentAllergyService.GetAll();
        return Response<IEnumerable<ResidentAllergyDTO>>.Ok(allergies, "Lista de alergias de residentes obtida com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var allergy = await _residentAllergyService.GetById(id);
        return Response<ResidentAllergyDTO>.Ok(allergy, "Alergia de residente obtida com sucesso!");
    }

    [HttpPost]
    public async Task<IActionResult> Post(ResidentAllergyDTO residentAllergyDto)
    {
        Execute.Executar(residentAllergyDto);
        residentAllergyDto.Id = 0;
        await _residentAllergyService.Create(residentAllergyDto);
        return Response<ResidentAllergyDTO>.Created(residentAllergyDto, "Alergia de residente cadastrada com sucesso!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ResidentAllergyDTO residentAllergyDto)
    {
        Execute.Executar(residentAllergyDto);
        await _residentAllergyService.Update(residentAllergyDto, id);
        return Response<ResidentAllergyDTO>.Ok(residentAllergyDto, "Alergia de residente atualizada com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _residentAllergyService.Remove(id);
        return Response<object>.NoContent();
    }
}
