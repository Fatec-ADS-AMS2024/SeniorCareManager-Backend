using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Contracts;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CarrierController : Controller
    {
        private readonly ICarrierService _carrierService;

        public CarrierController(ICarrierService service)
        {
            this._carrierService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
                var carriers = await _carrierService.GetAll();
                return Response<IEnumerable<CarrierDTO>>.Ok(carriers, "Lista de transportadoras obtidas com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
                var carriers = await _carrierService.GetById(id);
                return Response<CarrierDTO>.Ok(carriers, "Transportadora obtida com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarrierDTO carrierDto)
        {
                Execute.Executar(carrierDto);
                carrierDto.Id = 0;
                await _carrierService.Create(carrierDto);
                return Response<CarrierDTO>.Created(carrierDto, "Transportadora cadastrada com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CarrierDTO carrierDto)
        {
                Execute.Executar(carrierDto);
                await _carrierService.Update(carrierDto, id);
                return Response<CarrierDTO>.Ok(carrierDto, "Transportadora atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

                await _carrierService.Remove(id);
                return Response<object>.NoContent();
        }
    }
}
