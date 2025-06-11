using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;


namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AllergyController : Controller
    {
        private readonly IAllergyService _allergyService;
        private readonly Response _response;
        public AllergyController(IAllergyService allergyService)
        {
            this._allergyService = allergyService;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

                var allergies = await _allergyService.GetAll();
                _response.Code = ResponseEnum.Success;
                _response.Data = allergies;
                _response.Message = "Lista de alergias!";
                return Ok(_response);
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var allergy = await _allergyService.GetById(id);
                if (allergy == null) return NotFound("Alergia não encontrada!");
                return Ok(allergy);
            }
            catch (KeyNotFoundException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível adquirir a alergia.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(AllergyDTO allergyDTO)
        {
            try
            {
                allergyDTO.Id = 0;
                await _allergyService.Create(allergyDTO);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Alergia cadastrada com sucesso!";
                _response.Data = allergyDTO;
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = null;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = allergyDTO;
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível cadastrar a alergia.";
                _response.Data = allergyDTO;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            return Ok(_response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, AllergyDTO allergyDTO)
        {
            try
            {
                await _allergyService.Update(allergyDTO, id); ;
                _response.Code = ResponseEnum.Success;
                _response.Message = "Alergia alterada com sucesso!";
                _response.Data = allergyDTO;
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = null;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = allergyDTO;
                return Conflict(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível alterar a alergia!";
                _response.Data = allergyDTO;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _allergyService.Remove(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Alergia apagada com sucesso!";
                _response.Data = null;
            }
            catch (KeyNotFoundException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = null;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar apagar alergia.";
                _response.Data = null;
            }
            return Ok(_response);
        }
    }
}
