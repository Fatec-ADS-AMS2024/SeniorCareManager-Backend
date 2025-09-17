using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductTypeController : ControllerBase
{
    private readonly IProductTypeService _service;

    public ProductTypeController(IProductTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productTypes = await _service.GetAll();
        return Ok(productTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var productType = await _service.GetById(id);
        return Ok(productType);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductTypeDTO dto)
    {
        Execute.Executar(dto);
        await _service.Create(dto);
        return Ok(dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductTypeDTO dto)
    {
        Execute.Executar(dto);
        await _service.Update(dto, id);
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Remove(id);
        return Ok($"Tipo de produto com id {id} removido com sucesso.");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, ProductTypeDTO dto)
    {
        Execute.Executar(dto);
        await _service.Update(dto, id);
        return Ok(dto);
    }
}