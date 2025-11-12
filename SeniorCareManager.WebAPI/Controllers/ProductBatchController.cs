using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductBatchController : Controller
    {
        private readonly IProductBatchService _productBatchService;

        public ProductBatchController(IProductBatchService service)
        {
            this._productBatchService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _productBatchService.GetAll();
            return Response<IEnumerable<ProductBatchDTO>>.Ok(list, "Lista de lotes obtida com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var dto = await _productBatchService.GetById(id);
            return Response<ProductBatchDTO>.Ok(dto, "Lote obtido com sucesso!");
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProduct(long productId)
        {
            var list = await _productBatchService.GetByProduct(productId);
            return Response<IEnumerable<ProductBatchDTO>>.Ok(list, "Lotes por produto obtidos com sucesso!");
        }

        [HttpGet("expiring")]
        public async Task<IActionResult> GetExpiringBatches([FromQuery] int daysAhead = 30)
        {
            var list = await _productBatchService.GetExpiringBatches(daysAhead);
            return Response<IEnumerable<ProductBatchDTO>>.Ok(list, "Lotes próximos do vencimento obtidos com sucesso!");
        }

        [HttpGet("expired")]
        public async Task<IActionResult> GetExpiredBatches()
        {
            var list = await _productBatchService.GetExpiredBatches();
            return Response<IEnumerable<ProductBatchDTO>>.Ok(list, "Lotes vencidos obtidos com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductBatchDTO dto)
        {
            Execute.Executar(dto);
            dto.Id = 0;
            await _productBatchService.Create(dto);
            return Response<ProductBatchDTO>.Created(dto, "Lote cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, ProductBatchDTO dto)
        {
            Execute.Executar(dto);
            await _productBatchService.Update(dto, id);
            return Response<ProductBatchDTO>.Ok(dto, "Lote atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _productBatchService.Remove(id);
            return Response<object>.NoContent();
        }
    }
}
