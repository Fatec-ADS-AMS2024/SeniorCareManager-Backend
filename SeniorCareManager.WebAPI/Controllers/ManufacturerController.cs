using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IManufacturerService service)
        {
            _manufacturerService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Response<object>.Ok(await _manufacturerService.GetAll(), "Fabricantes obtidos com sucesso!");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Response<object>.Ok(await _manufacturerService.GetById(id), "Fabricante obtido com sucesso!");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ManufacturerDTO manufacturer)
        {
            Execute.Executar(manufacturer);
            return Response<object>.Created(await _manufacturerService.Create(manufacturer), "Fabricante cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ManufacturerDTO manufacturer)
        {
            Execute.Executar(manufacturer);
            await _manufacturerService.Update(manufacturer, id);
            return Response<object>.Ok(manufacturer, "Fabricante atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _manufacturerService.Remove(id);
            return Response<object>.NoContent();
        }
    }
}