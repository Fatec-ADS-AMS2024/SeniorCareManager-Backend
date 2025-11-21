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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService service)
        {
            this._supplierService = service;
        }

        [HttpGet, MapToApiVersion("1")]
        public async Task<IActionResult> Get()
        {
            return Response<object>.Ok(await _supplierService.GetAll(), "Fornecedores obtidos com sucesso!");
        }

        [HttpGet("{id}"), MapToApiVersion("1")]
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
