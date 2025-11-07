using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class ProductGroupController : Controller
{
    private readonly IProductGroupService _service;

    public ProductGroupController(IProductGroupService service)
    {
        _service = service;
    }

    [HttpGet, MapToApiVersion("1")]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var groups = await _service.GetAll();
        return Response<IEnumerable<ProductGroupDTO>>.Ok(groups, "Lista de grupos de produto obtida com sucesso!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var group = await _service.GetById(id);
        return Response<ProductGroupDTO>.Ok(group, "Grupo de produto obtido com sucesso!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(ProductGroupDTO dto)
    {
        Execute.Executar(dto);
        return Response<ProductGroupDTO>.Created(await _service.Create(dto), "Grupo de produto cadastrado com sucesso!");
    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(int id, ProductGroupDTO dto)
    {
        Execute.Executar(dto);
        await _service.Update(dto, id);
        return Response<ProductGroupDTO>.Ok(dto, "Grupo de produto atualizado com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Remove(id);
        return Response<object>.Ok(new { Id = id }, "Grupo de produto excluído com sucesso!");
    }
}
