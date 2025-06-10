using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductGroupController : ControllerBase
{
    private readonly IProductGroupService _productGroupService;

    public ProductGroupController(IProductGroupService service)
    {
        _productGroupService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var groups = await _productGroupService.GetAll();

        return Ok(new Response
        {
            Code = ResponseEnum.Success,
            Message = "Lista de grupos de produto!",
            Data = groups
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var group = await _productGroupService.GetById(id);

            if (group is null)
            {
                return NotFound(new Response
                {
                    Code = ResponseEnum.NotFound,
                    Message = "Grupo de produto não encontrado.",
                    Data = null
                });
            }

            return Ok(new Response
            {
                Code = ResponseEnum.Success,
                Message = $"Grupo de produto \"{group.Name}\" obtido com sucesso!",
                Data = group
            });
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Code = ResponseEnum.Error,
                Message = "Não foi possível adquirir o grupo de produto.",
                Data = null
            });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductGroupDTO productGroup)
    {
        if (string.IsNullOrWhiteSpace(productGroup.Name))
        {
            return BadRequest(new Response
            {
                Code = ResponseEnum.Invalid,
                Message = "Nome inválido.",
                Data = null
            });
        }

        if (await _productGroupService.IsDuplicateNameAsync(productGroup.Name))
        {
            return Conflict(new Response
            {
                Code = ResponseEnum.Conflict,
                Message = "Nome duplicado.",
                Data = productGroup
            });
        }

        try
        {
            productGroup.Id = 0;
            await _productGroupService.Create(productGroup);

            return Ok(new Response
            {
                Code = ResponseEnum.Success,
                Message = "Grupo de produto cadastrado com sucesso!",
                Data = productGroup
            });
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Code = ResponseEnum.Error,
                Message = "Erro ao cadastrar grupo de produto.",
                Data = productGroup
            });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductGroupDTO productGroup)
    {
        if (string.IsNullOrWhiteSpace(productGroup.Name))
        {
            return BadRequest(new Response
            {
                Code = ResponseEnum.Invalid,
                Message = "Nome inválido.",
                Data = productGroup
            });
        }

        if (await _productGroupService.IsDuplicateNameAsync(productGroup.Name, id))
        {
            return Conflict(new Response
            {
                Code = ResponseEnum.Conflict,
                Message = "Nome duplicado.",
                Data = productGroup
            });
        }

        try
        {
            await _productGroupService.Update(productGroup, id);

            return Ok(new Response
            {
                Code = ResponseEnum.Success,
                Message = "Grupo de produto alterado com sucesso!",
                Data = productGroup
            });
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Code = ResponseEnum.Error,
                Message = "Erro ao atualizar grupo de produto.",
                Data = productGroup
            });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var group = await _productGroupService.GetById(id);
            if (group is null)
            {
                return NotFound(new Response
                {
                    Code = ResponseEnum.NotFound,
                    Message = "Grupo de produto não encontrado.",
                    Data = null
                });
            }

            await _productGroupService.Remove(id);

            return Ok(new Response
            {
                Code = ResponseEnum.Success,
                Message = "Grupo de produto apagado com sucesso!",
                Data = null
            });
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Code = ResponseEnum.Error,
                Message = "Erro ao tentar remover grupo de produto.",
                Data = null
            });
        }
    }
}
