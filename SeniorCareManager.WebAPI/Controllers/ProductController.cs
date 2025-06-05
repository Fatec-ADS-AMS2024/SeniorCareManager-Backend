using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService service)
    {
        this._productService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var product = await _productService.GetAll();
        return Ok(product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetById(id);
        if (product == null) return NotFound("Produto não encontrado!");
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product product)
    {
        try
        {
            await _productService.Create(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo grupo de produto.");
        }
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Product product)
    {
        try
        {
            await _productService.Update(product, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar o produto: " + ex.Message);
        }

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.Remove(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover o grupo de produto.");
        }

        return Ok("Grupo de produto apagado com sucesso");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, Product product)
    {
        try
        {
            await _productService.Update(product, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover o grupo do produto.");
        }

        return Ok(product);
    }

}