using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
// [Authorize]
public class ProductTypeController : ControllerBase
{
    private readonly IProductTypeService _service;

    public ProductTypeController(IProductTypeService service)
    {
        _service = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> GetAll()
    {
        var productTypes = await _service.GetAll();
        return Response<IEnumerable<ProductTypeDTO>>.Ok(productTypes, "Lista de tipos de produto obtida com sucesso!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {
        var productType = await _service.GetById(id);
        return Response<ProductTypeDTO>.Ok(productType, "Tipo de produto encontrado!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Create([FromBody] ProductTypeDTO dto)
    {
        Execute.Executar(dto);
        dto.Id = 0;
        return Response<ProductTypeDTO>.Created(await _service.Create(dto), "Tipo de produto cadastrado com sucesso!");
    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductTypeDTO dto)
    {
        Execute.Executar(dto);
        await _service.Update(dto, id);
        return Response<ProductTypeDTO>.Ok(dto, "Tipo de produto atualizado com sucesso!");
    }

    [HttpPatch("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Patch(int id, [FromBody] ProductTypeDTO dto)
    {
        Execute.Executar(dto);
        await _service.Update(dto, id);
        return Response<ProductTypeDTO>.Ok(dto, "Tipo de produto atualizado com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Remove(id);
        return Response<object>.NoContent();
    }
}
