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
        private readonly Response _response;

        public CarrierController(ICarrierService service)
        {
            this._carrierService = service;
            _response = new Response();
        }
        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var carriers = await _carrierService.GetAll();
                _response.Code = ResponseEnum.Success;
                _response.Data = carriers;
                _response.Message = "Lista de transportadoras carregada com sucesso.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = ex.Message;
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var carriers = await _carrierService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Transportadora " + carriers.TradeName + " encontrada com sucesso";
                _response.Data = carriers;
                return Ok(_response);
            }
            catch (KeyNotFoundException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível consultar a transportadora.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarrierDTO carrierDto)
        {
            try
            {
                Execute.Executar(carrierDto);
                carrierDto.Id = 0;
                await _carrierService.Create(carrierDto);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Transportadora cadastrada com sucesso!";
                _response.Data = carrierDto;
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = carrierDto;
                return NotFound(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = carrierDto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = carrierDto;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível cadastrar a transportadora";
                _response.Data = carrierDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            return Ok(_response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CarrierDTO carrierDto)
        {
            try
            {
                Execute.Executar(carrierDto);
                await _carrierService.Update(carrierDto, id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Transportadora atualizada com sucesso!";
                _response.Data = carrierDto;
            }
            catch(ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = carrierDto;
                return NotFound(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = carrierDto;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Message = ex.Message;
                _response.Data = carrierDto;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível atualizar a transportadora.";
                _response.Data = carrierDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
            return Ok(_response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _carrierService.Remove(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Transportadora removida com sucesso.";
                _response.Data = null;
            }
            catch (KeyNotFoundException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = null;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível remover a transportadora.";
                _response.Data = null;
            }
            return Ok(_response);
        }
    }
}
