using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController: Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService service)
    {
        this._productService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAll();
        return Response<IEnumerable<ProductDTO>>.Ok(products, "Lista de produtos obtidos com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetById(id);
        
        return Response<ProductDTO>.Ok(product, "Produto obtido com sucesso!");
    
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductDTO productDto)
    {
        Execute.Executar(productDto);
        productDto.Id = 0;
        await _productService.Create(productDto);

        return Response<ProductDTO>.Created(productDto, "Produto Cadastrado com sucesso!"); 

    }

    [HttpPut("{id}")] 
    public async Task<IActionResult> Put(int id, ProductDTO productDto)
    {
        Execute.Executar(productDto);
        await _productService.Update(productDto, id); ;

        return Response<ProductDTO>.Ok(productDto, "Produto atualizado com sucesso!"); 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {

        await _productService.Remove(id);

        return Response<object>.NoContent("Grupo de produto apagado com sucesso!");
    }
}