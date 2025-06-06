using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;
using SeniorCareManager.WebAPI.Objects.Contracts;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productervice;
    private readonly Response _response;

    public ProductController(IProductService service)
    {
        this._productervice = service;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var product = await _productervice.GetAll();
        _response.Code = ResponseEnum.Success;
        _response.Data = product;
        _response.Message = "Lista de produtos!";
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var product = await _productervice.GetById(id);
            if (product is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "produto não encontrado.";
                _response.Data = product;
                return NotFound(_response);
            }

            _response.Code = ResponseEnum.Success;
            _response.Message = "Produto " + product.GenericName + " obtido com sucesso!";
            _response.Data = product;
            return Ok(_response);

        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Message = "Não foi possível adquirir o Produto.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductDTO productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.GenericName))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "Dados inválidos.";

            return BadRequest(_response);
        }
        var product = await _productervice.GetAll();
        if (CheckDuplicates(product, productDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = productDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            productDto.Id = 0;
            await _productervice.Create(productDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Produto Cadastrado com sucesso!";
            _response.Data = productDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível cadastrar o produto.";
            _response.Data = productDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProductDTO productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.GenericName))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = productDto;
            _response.Message = "Nome inválido.";
            return BadRequest(_response);
        }
        var product = await _productervice.GetAll();
        if (CheckDuplicates(product, productDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = productDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            await _productervice.Update(productDto, id); ;
            _response.Code = ResponseEnum.Success;
            _response.Message = "Produto alterado com sucesso!";
            _response.Data = productDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar o Produto!";
            _response.Data = productDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var product = await _productervice.GetById(id);
            if (product is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = null;
                _response.Message = "O Produto não foi encontrado.";
                return NotFound(_response);
            }
            await _productervice.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Produto apagado com sucesso!";
            _response.Data = null;
        }

        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar apagar grupo de Produto.";
            _response.Data = null;
        }
        return Ok(_response);

    }
    private static bool CheckDuplicates(IEnumerable<ProductDTO> productDTO, ProductDTO productDTO)
    {
        foreach (var product in productDTO)
        {
            if (productDTO.Id == product.Id)
            {
                continue;
            }

            if (StringValidator.CompareString(productDTO.GenericName, product.Ge))
            {
                return true;
            }
        }
        return false;
    }


}