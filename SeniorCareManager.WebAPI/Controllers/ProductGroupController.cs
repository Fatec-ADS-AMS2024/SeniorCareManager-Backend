using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductGroupController : Controller
{
    private readonly IProductGroupService _productGroupService;
    private readonly Response _response;

    public ProductGroupController(IProductGroupService service)
    {
        _productGroupService = service;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var groups = await _productGroupService.GetAll();
        _response.Code = ResponseEnum.Success;
        _response.Data = groups;
        _response.Message = "Lista de grupos de produto!";
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var group = await _productGroupService.GetById(id);
            if (group is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "Grupo de produto não encontrado.";
                _response.Data = null;
                return NotFound(_response);
            }

            _response.Code = ResponseEnum.Success;
            _response.Message = $"Grupo de produto \"{group.Name}\" obtido com sucesso!";
            _response.Data = group;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível adquirir o grupo de produto.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductGroupDTO productGroup)
    {
        if (string.IsNullOrWhiteSpace(productGroup.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = "Nome inválido.";
            _response.Data = null;
            return BadRequest(_response);
        }

        var existingGroups = await _productGroupService.GetAll();
        if (CheckDuplicates(existingGroups, productGroup))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Message = "Nome duplicado.";
            _response.Data = productGroup;
            return BadRequest(_response);
        }

        try
        {
            productGroup.Id = 0;
            await _productGroupService.Create(productGroup);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Grupo de produto cadastrado com sucesso!";
            _response.Data = productGroup;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao cadastrar grupo de produto.";
            _response.Data = productGroup;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProductGroupDTO productGroup)
    {
        if (string.IsNullOrWhiteSpace(productGroup.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = "Nome inválido.";
            _response.Data = productGroup;
            return BadRequest(_response);
        }

        var existingGroups = await _productGroupService.GetAll();
        if (CheckDuplicates(existingGroups, productGroup))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Message = "Nome duplicado.";
            _response.Data = productGroup;
            return BadRequest(_response);
        }

        try
        {
            await _productGroupService.Update(productGroup, id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Grupo de produto alterado com sucesso!";
            _response.Data = productGroup;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao atualizar grupo de produto.";
            _response.Data = productGroup;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
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
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "Grupo de produto não encontrado.";
                _response.Data = null;
                return NotFound(_response);
            }

            await _productGroupService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Grupo de produto apagado com sucesso!";
            _response.Data = null;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar remover grupo de produto.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    private static bool CheckDuplicates(IEnumerable<ProductGroupDTO> groups, ProductGroupDTO currentGroup)
    {
        foreach (var group in groups)
        {
            if (group.Id == currentGroup.Id)
                continue;

            if (StringValidator.CompareString(group.Name, currentGroup.Name))
                return true;
        }
        return false;
    }
}
