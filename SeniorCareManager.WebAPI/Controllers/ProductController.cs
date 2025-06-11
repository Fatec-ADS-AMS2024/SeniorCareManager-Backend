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
    private readonly IProductService _productService;
    private readonly Response _response;

    public ProductController(IProductService service)
    {
        this._productService = service;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var product = await _productService.GetAll();
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
            var product = await _productService.GetById(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "produto " + product.GenericName + " obtido com sucesso!";
            _response.Data = product;
            return Ok(_response);
        }
        catch (KeyNotFoundException ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Message = ex.Message;
            _response.Data = null;
            return NotFound(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível adquirir o produto.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Post(ProductDTO productDto)
    {
        try
        {
            productDto.Id = 0;
            await _productService.Create(productDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Produto Cadastrado com sucesso!";
            _response.Data = productDto;
        }
        catch (ArgumentException ex)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = ex.Message;
            _response.Data = null;
            return BadRequest(_response);
        }
        catch (InvalidOperationException ex)
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Message = ex.Message;
            _response.Data = productDto;
            return Conflict(_response);
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
        try
        {
            await _productService.Update(productDto, id); ;
            _response.Code = ResponseEnum.Success;
            _response.Message = "produto alterado com sucesso!";
            _response.Data = productDto;
        }
        catch (ArgumentException ex)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = ex.Message;
            _response.Data = null;
            return BadRequest(_response);
        }
        catch (InvalidOperationException ex)
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Message = ex.Message;
            _response.Data = productDto;
            return Conflict(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar o produto!";
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
            await _productService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Grupo de produto apagado com sucesso!";
            _response.Data = null;
        }
        catch (KeyNotFoundException ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Data = null;
            _response.Message = ex.Message;
            return NotFound(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar apagar grupo de produto.";
            _response.Data = null;
        }
        return Ok(_response);
    }
}