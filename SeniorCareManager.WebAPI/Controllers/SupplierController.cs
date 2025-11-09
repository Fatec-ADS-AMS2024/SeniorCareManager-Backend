using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Response<object>.Ok(await _supplierService.GetAll(), "Fornecedores obtidos com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Response<object>.Ok(await _supplierService.GetById(id), "Fornecedor obtido com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(SupplierDTO supplier)
        {
            Execute.Executar(supplier);
            return Response<object>.Created(await _supplierService.Create(supplier), "Fornecedor cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SupplierDTO supplier)
        {
            Execute.Executar(supplier);
            await _supplierService.Update(supplier, id);
            return Response<object>.Ok(supplier, "Fornecedor atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierService.Remove(id);
            return Response<object>.NoContent();
        }
    }
}