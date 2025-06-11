using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var suppliers = await _supplierService.GetAll();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar fornecedores: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetById(id);
                if (supplier == null)
                    return NotFound("Fornecedor não encontrado.");

                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar fornecedor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(SupplierDTO supplier)
        {
            var (isValid, errorMessage) = await supplier.ValidateAsync(_supplierService);
            if (!isValid)
                return BadRequest(errorMessage);

            try
            {
                await _supplierService.Create(supplier);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao inserir fornecedor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, SupplierDTO supplier)
        {
            supplier.Id = id;

            var (isValid, errorMessage) = await supplier.ValidateAsync(_supplierService, isUpdate: true);
            if (!isValid)
                return BadRequest(errorMessage);

            try
            {
                await _supplierService.Update(supplier, id);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar fornecedor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var supplier = await _supplierService.GetById(id);
                if (supplier == null)
                    return NotFound("Fornecedor não encontrado.");

                await _supplierService.Remove(id);
                return Ok("Fornecedor deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar fornecedor: {ex.Message}");
            }
        }
    }
}
