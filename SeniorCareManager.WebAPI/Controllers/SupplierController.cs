using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using System.ComponentModel.DataAnnotations;

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
            var suppliers = await _supplierService.GetAll();
            return Response<object>.Ok(suppliers, "Lista de Fornecedores!");
        }

        [HttpGet("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> GetById(int id)
        {
            var suppliers = await _supplierService.GetById(id);
            return Response<object>.Ok(suppliers, "Fornecedores encontrados!");
        }

        [HttpPost, MapToApiVersion("1")]
        public async Task<IActionResult> Post([FromBody] SupplierDTO supplierDto)
        {
            supplierDto.Id = 0;
            ValidateDto(supplierDto);
            var created = await _supplierService.Create(supplierDto);
            return Response<object>.Created(created, "Fornecedor cadastrado com sucesso!");
        }

        [HttpPut("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Put(int id, [FromBody] SupplierDTO supplierDto)
        {
            ValidateDto(supplierDto);
            await _supplierService.Update(supplierDto, id);
            return Response<object>.Ok(supplierDto, "Fornecedor alterado com sucesso!");
        }

        [HttpDelete("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplierService.Remove(id);
            return Response<object>.NoContent();
        }

        // Validação local usando DataAnnotations (substitui o Execute.Executar)
        private void ValidateDto<T>(T dto)
        {
            if (dto == null) throw new ExceptionBadRequest("Objeto de entrada nulo.");

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

            if (!isValid)
            {
                var message = string.Join(" | ", results.Select(r => r.ErrorMessage));
                throw new ExceptionBadRequest(message);
            }
        }
    }
}