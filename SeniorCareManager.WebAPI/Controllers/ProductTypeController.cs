using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductTypeController : Controller
{
    private readonly IProductTypeService _productTypeService;
    private readonly Response _response;

    public ProductTypeController(IProductTypeService service)
    {
        _productTypeService = service;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var productTypes = await _productTypeService.GetAll();
        _response.Code = ResponseEnum.Success;
        _response.Data = productTypes;
        _response.Message = "Lista de tipos de produto!";
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var productType = await _productTypeService.GetById(id);
            if (productType is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "Tipo de produto não encontrado.";
                _response.Data = null;
                return NotFound(_response);
            }

            _response.Code = ResponseEnum.Success;
            _response.Message = $"Tipo de produto \"{productType.Name}\" obtido com sucesso!";
            _response.Data = productType;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao buscar tipo de produto.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductTypeDTO productType)
    {
        if (string.IsNullOrWhiteSpace(productType.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = "O campo 'Name' é obrigatório.";
            _response.Data = null;
            return BadRequest(_response);
        }

        if (productType.ProductGroupId <= 0)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = "O campo 'ProductGroupId' é obrigatório e deve ser maior que zero.";
            _response.Data = null;
            return BadRequest(_response);
        }

        var existingTypes = await _productTypeService.GetAll();
        var hasDuplicate = existingTypes.Any(pt =>
            StringValidator.CompareString(pt.Name, productType.Name));

        if (hasDuplicate)
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Message = "Já existe um tipo de produto com esse nome.";
            _response.Data = productType;
            return Conflict(_response);
        }

        try
        {
            productType.Id = 0;
            await _productTypeService.Create(productType);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Tipo de produto cadastrado com sucesso!";
            _response.Data = productType;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar cadastrar o tipo de produto.";
            _response.Data = productType;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ProductTypeDTO productType)
    {
        if (string.IsNullOrWhiteSpace(productType.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = "O campo 'Name' é obrigatório.";
            _response.Data = productType;
            return BadRequest(_response);
        }

        if (productType.ProductGroupId <= 0)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = "O campo 'ProductGroupId' é obrigatório e deve ser maior que zero.";
            _response.Data = productType;
            return BadRequest(_response);
        }

        var existingTypes = await _productTypeService.GetAll();
        var hasDuplicate = existingTypes.Any(pt =>
            pt.Id != id && StringValidator.CompareString(pt.Name, productType.Name));

        if (hasDuplicate)
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Message = "Já existe um tipo de produto com esse nome.";
            _response.Data = productType;
            return Conflict(_response);
        }

        try
        {
            await _productTypeService.Update(productType, id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Tipo de produto atualizado com sucesso!";
            _response.Data = productType;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar atualizar o tipo de produto.";
            _response.Data = productType;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var productType = await _productTypeService.GetById(id);
            if (productType == null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "Tipo de produto não encontrado.";
                _response.Data = null;
                return NotFound(_response);
            }

            await _productTypeService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Tipo de produto removido com sucesso!";
            _response.Data = null;
            return Ok(_response);
        }
        catch
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar remover o tipo de produto.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] ProductTypeDTO productType)
    {
        return await Put(id, productType);
    }
}