using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ManufacturerController : Controller
    {
        private readonly IManufacturerService _manufacturerService;
        private readonly Response _response;

        public ManufacturerController(IManufacturerService service)
        {
            this._manufacturerService = service;
            _response = new Response();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var manufacturers = await _manufacturerService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Data = manufacturers;
            _response.Message = "Lista de fabricantes obtida com sucesso!";
            return Ok(_response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var manufacturer = await _manufacturerService.GetById(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Fabricante " + manufacturer.CorporateName + " obtido com sucesso!";
                _response.Data = manufacturer;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = ex.Message;
                _response.Data = null;
                return NotFound(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível obter o fabricante.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ManufacturerDTO manufacturerDto)
        {
            try
            {
                manufacturerDto.Id = 0;
                await _manufacturerService.Create(manufacturerDto);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Fabricante cadastrado com sucesso!";
                _response.Data = manufacturerDto;

                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Message = ex.Message;
                _response.Data = null;
                return BadRequest(_response);
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
                _response.Data = manufacturerDto;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível cadastrar o fabricante.";
                _response.Data = manufacturerDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ManufacturerDTO manufacturerDto)
        {
            try
            {
                await _manufacturerService.Update(manufacturerDto, id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Fabricante atualizado com sucesso!";
                _response.Data = manufacturerDto;
                return Ok(_response);
            }
            catch (ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = manufacturerDto;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (ArgumentException ex)
            {
                _response.Code = ResponseEnum.Invalid;
                _response.Data = manufacturerDto;
                _response.Message = ex.Message;
                return BadRequest(_response);
            }
            catch (InvalidOperationException ex)
            {
                _response.Code = ResponseEnum.Conflict;
                _response.Data = manufacturerDto;
                _response.Message = ex.Message;
                return Conflict(_response);
            }
            catch (Exception)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Não foi possível atualizar o fabricante!";
                _response.Data = manufacturerDto;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _manufacturerService.Remove(id);
                _response.Code = ResponseEnum.Success;
                _response.Message = "Fabricante excluído com sucesso!";
                _response.Data = null;
                return Ok(_response);
            }
            catch(ArgumentNullException ex)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = null;
                _response.Message = ex.Message;
                return NotFound(_response);
            }
            catch (Exception ex)
            {
                _response.Code = ResponseEnum.Error;
                _response.Message = "Erro ao tentar excluir o fabricante.";
                _response.Data = null;
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
