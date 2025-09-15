using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService service)
        {
            _manufacturerService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var manufacturers = await _manufacturerService.GetAll();
            return Response<IEnumerable<ManufacturerDTO>>.Ok(manufacturers, "Lista de fabricantes obtida com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var manufacturer = await _manufacturerService.GetById(id);
            return Response<ManufacturerDTO>.Ok(manufacturer, "Fabricante obtido com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ManufacturerDTO manufacturer)
        {
            Execute.Executar(manufacturer);
            manufacturer.Id = 0;
            await _manufacturerService.Create(manufacturer);

            return Response<ManufacturerDTO>.Created(manufacturer, "Fabricante cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ManufacturerDTO manufacturer)
        {
            Execute.Executar(manufacturer);
            await _manufacturerService.Update(manufacturer, id);

            return Response<ManufacturerDTO>.Ok(manufacturer, "Fabricante atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _manufacturerService.Remove(id);
            return Response<object>.NoContent("Fabricante apagado com sucesso!");
        }
    }
}