using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class CompanyController : Controller
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService service)
    {
        this._companyService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var companies = await _companyService.GetAll();
        return Response<object>.Ok(companies, "Lista de empresas obtida com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var company = await _companyService.GetById(id);
        return Response<object>.Ok(company, $"Empresa encontrada!");
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CompanyDTO companyDto)
    {
        companyDto.Id = 0;
        await _companyService.Create(companyDto);
        return Response<object>.Created(companyDto, "Empresa cadastrada com sucesso!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] CompanyDTO companyDto)
    {
        await _companyService.Update(companyDto, id);
        return Response<object>.Ok(companyDto, "Empresa atualizada com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _companyService.Remove(id);
        return Response<object>.NoContent("Empresa excluída com sucesso!");
    }

    [HttpPost("{id}/upload-logo")]
    public async Task<IActionResult> InsertLogo(int id, IFormFile logo)
    {
        await _companyService.UpdateLogo(id, logo);
        return Response<object>.Ok(null, "Logo inserido com sucesso!");
    }

    [HttpGet("{id}/logo-base64")]
    public async Task<IActionResult> GetCompanyLogoBase64(int id)
    {
        var result = await _companyService.GetLogoBase64(id);
        if (result.Base64 is null)
            return Response<object>.Ok(null, "Logo não encontrado.");

        var payload = new
        {
            Base64 = result.Base64
        };

        return Response<object>.Ok(payload, "Logo obtido com sucesso.");
    }
}
