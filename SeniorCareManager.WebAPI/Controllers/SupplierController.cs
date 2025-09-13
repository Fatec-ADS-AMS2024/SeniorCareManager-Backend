using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService service)
        {
            this._supplierService = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var suppliers = await _supplierService.GetAll();
            return Response<object>.Ok(suppliers, "Lista de Fornecedores!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var suppliers = await _supplierService.GetById(id);
            return Response<object>.Ok(suppliers, "Fornecedores encontrados!");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SupplierDTO supplierDto)
        {
            supplierDto.Id = 0;
            Execute.Executar(supplierDto);
            await _supplierService.Create(supplierDto);
            return Response<object>.Created(supplierDto, "Fornecedor cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SupplierDTO supplierDto)
        {
            await _supplierService.Update(supplierDto, id);
            Execute.Executar(supplierDto);
            return Response<object>.Ok(supplierDto, "Fornecedor alterada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierService.Remove(id);
            return Response<object>.NoContent("O fornecedor foi apagado com sucesso!");
        }
    }
}