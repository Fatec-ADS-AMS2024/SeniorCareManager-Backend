using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Authorize]
    public class ProductBatchController : Controller
    {
        private readonly IProductBatchService _productBatchService;

        public ProductBatchController(IProductBatchService service)
        {
            this._productBatchService = service;
        }

        [HttpGet, MapToApiVersion("1")]
        public async Task<IActionResult> Get()
        {
            var productBatches = await _productBatchService.GetAll();
            return Response<IEnumerable<ProductBatchDTO>>.Ok(productBatches, "Lista de lotes obtida com sucesso!");
        }

        [HttpGet("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> GetById(long id)
        {
            var productBatch = await _productBatchService.GetById(id);
            return Response<ProductBatchDTO>.Ok(productBatch, "Lote obtido com sucesso!");
        }

        [HttpGet("product/{productId}"), MapToApiVersion("1")]
        public async Task<IActionResult> GetByProduct(long productId)
        {
            var productBatches = await _productBatchService.GetByProduct(productId);
            return Response<IEnumerable<ProductBatchDTO>>.Ok(productBatches, "Lotes por produto obtidos com sucesso!");
        }

        [HttpGet("expiring"), MapToApiVersion("1")]
        public async Task<IActionResult> GetExpiringBatches([FromQuery] int daysAhead = 30)
        {
            var list = await _productBatchService.GetExpiringBatches(daysAhead);
            return Response<IEnumerable<ProductBatchDTO>>.Ok(list, "Lotes próximos do vencimento obtidos com sucesso!");
        }

        [HttpGet("expired"), MapToApiVersion("1")]
        public async Task<IActionResult> GetExpiredBatches()
        {
            var list = await _productBatchService.GetExpiredBatches();
            return Response<IEnumerable<ProductBatchDTO>>.Ok(list, "Lotes vencidos obtidos com sucesso!");
        }

        [HttpPost, MapToApiVersion("1")]
        public async Task<IActionResult> Post(ProductBatchDTO productBatchDto)
        {
            Execute.Executar(productBatchDto);
            productBatchDto.Id = 0;
            return Response<ProductBatchDTO>.Created(await _productBatchService.Create(productBatchDto), "Lote cadastrado com sucesso!");
        }

        [HttpPut("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Put(long id, ProductBatchDTO productBatchDto)
        {
            Execute.Executar(productBatchDto);
            await _productBatchService.Update(productBatchDto, id);
            return Response<ProductBatchDTO>.Ok(productBatchDto, "Lote atualizado com sucesso!");
        }

        [HttpDelete("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Delete(long id)
        {
            await _productBatchService.Remove(id);
            return Response<object>.NoContent();
        }
    }
}
