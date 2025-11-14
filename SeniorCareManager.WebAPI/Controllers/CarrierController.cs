using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Contracts;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarrierController : Controller
    {

        private readonly ICarrierService _carrierService;

        public CarrierController(ICarrierService carrierService)
        {
            this._carrierService = carrierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carriers = await _carrierService.GetAll();
            return Response<object>.Ok(carriers, "Lista de transportadoras obtida com sucesso!");
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var carriers = await _carrierService.GetById(id);
            return Response<object>.Ok(carriers, "Transportadora obtida com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarrierDTO carrier)
        {
            return Response<object>.Created(await _carrierService.Create(carrier), "Transportadora cadastrada com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CarrierDTO carrier)
        {
            await _carrierService.Update(carrier, id);
            return Response<object>.Ok(carrier, "Transportadora atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _carrierService.Remove(id);
            return Response<object>.NoContent();
        }
    }
}