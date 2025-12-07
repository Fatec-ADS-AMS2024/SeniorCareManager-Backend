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
    public class UnitOfMeasureController : Controller
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;

        public UnitOfMeasureController(IUnitOfMeasureService service)
        {
            this._unitOfMeasureService = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var unitofmeasures = await _unitOfMeasureService.GetAll();
            return Response<IEnumerable<UnitOfMeasureDTO>>.Ok(unitofmeasures, "Lista de unidade de medidas obtidas com sucesso!");
        }

        [HttpGet("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> GetById(int id)
        {
            var unitofmeasure = await _unitOfMeasureService.GetById(id);

            return Response<UnitOfMeasureDTO>.Ok(unitofmeasure, "Unidade de media obtida com sucesso!");

        }

        [HttpPost, MapToApiVersion("1")]
        public async Task<IActionResult> Post(UnitOfMeasureDTO unitofmeasureDto)
        {
            Execute.Executar(unitofmeasureDto);
            unitofmeasureDto.Id = 0;

            return Response<UnitOfMeasureDTO>.Created(await _unitOfMeasureService.Create(unitofmeasureDto), "Unidade de medida Cadastrada com sucesso!");

        }

        [HttpPut("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Put(int id, UnitOfMeasureDTO unitofmeasureDto)
        {
            Execute.Executar(unitofmeasureDto);
            await _unitOfMeasureService.Update(unitofmeasureDto, id);

            return Response<UnitOfMeasureDTO>.Ok(unitofmeasureDto, "Unidade de mediada atualizada com sucesso!");
        }

        [HttpDelete("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Delete(int id)
        {

            await _unitOfMeasureService.Remove(id);

            return Response<object>.NoContent();
        }
    }
}
