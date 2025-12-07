using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    // [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService service)
        {
            _supplierService = service;
        }

        [HttpGet, MapToApiVersion("1")]
        public async Task<IActionResult> Get()
        {
            var suppliers = await _supplierService.GetAll();
            return Response<object>.Ok(suppliers, "Fornecedores obtidos com sucesso!");
        }

        [HttpGet("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> GetById(int id)
        {
            var supplier = await _supplierService.GetById(id);
            return Response<object>.Ok(supplier, "Fornecedor obtido com sucesso!");
        }

        [HttpPost, MapToApiVersion("1")]
        public async Task<IActionResult> Post([FromBody] SupplierDTO supplier)
        {
            Execute.Executar(supplier);
            supplier.Id = 0;
            var created = await _supplierService.Create(supplier);
            return Response<object>.Created(created, "Fornecedor cadastrado com sucesso!");
        }

        [HttpPut("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Put(int id, [FromBody] SupplierDTO supplierDto)
        {
            Execute.Executar(supplierDto);
            await _supplierService.Update(supplierDto, id);
            return Response<object>.Ok(supplierDto, "Fornecedor atualizado com sucesso!");
        }

        [HttpDelete("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierService.Remove(id);
            return Response<object>.NoContent();
        }
    }
}

