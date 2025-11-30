using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;


namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [Authorize]
    public class AllergyController : Controller
    {
        private readonly IAllergyService _allergyService;
        public AllergyController(IAllergyService allergyService)
        {
            this._allergyService = allergyService;
        }

        [HttpGet, MapToApiVersion("1")]
        public async Task<IActionResult> GetAll()
        {
                var allergies = await _allergyService.GetAll();
                return Response<IEnumerable<AllergyDTO>>.Ok(allergies, "Alergias obtidas com sucesso!");
        }

        [HttpGet("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> GetById(int id)
        {
                var allergies = await _allergyService.GetById(id);
                return Response<AllergyDTO>.Ok(allergies, "Alergia obtida com sucesso!");
        }

        [HttpPost, MapToApiVersion("1")]
        public async Task<IActionResult> Post(AllergyDTO allergyDTO)
        {
                Execute.Executar(allergyDTO);
                allergyDTO.Id = 0;
                await _allergyService.Create(allergyDTO);
                return Response<AllergyDTO>.Created(allergyDTO, "Alergia criada com sucesso!");
        }

        [HttpPut("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Put(int id, AllergyDTO allergyDTO)
        {
                Execute.Executar(allergyDTO);
                await _allergyService.Update(allergyDTO, id); ;
                return Response<AllergyDTO>.Ok(allergyDTO, "Alergia atualizada com sucesso!");
        }

        [HttpDelete("{id}"), MapToApiVersion("1")]
        public async Task<IActionResult> Delete(int id)
        {
                await _allergyService.Remove(id);
                return Response<object>.NoContent();
        }
    }
}
