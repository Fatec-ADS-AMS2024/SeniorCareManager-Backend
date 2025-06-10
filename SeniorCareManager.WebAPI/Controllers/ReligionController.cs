using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class ReligionController : Controller
{
    private readonly IReligionService _religionService;
    private readonly Response _response;

    public ReligionController(IReligionService service)
    {
        this._religionService = service;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var religion = await _religionService.GetAll();
        _response.Code = ResponseEnum.Success;
        _response.Data = religion;
        _response.Message = "Lista de religiões!";
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var position = await _religionService.GetById(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Cargo " + position.Name + " obtido com sucesso!";
            _response.Data = position;
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
            _response.Message = "Não foi possível adquirir o cargo.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Post(ReligionDTO religionDto)
    {
        try
        {
            religionDto.Id = 0;
            await _religionService.Create(religionDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião cadastrada com sucesso!";
            _response.Data = religionDto;

            return Ok(_response);
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
            _response.Data = religionDto;
            return Conflict(_response);
        }
        catch (KeyNotFoundException ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = ex.Message;
            _response.Data = religionDto;
            return NotFound(_response);
        }
        catch (Exception)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível cadastrar a Religião.";
            _response.Data = religionDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ReligionDTO religionDto)
    {
        try
        {
            await _religionService.Update(religionDto, id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião alterada com sucesso!";
            _response.Data = religionDto;
            return Ok(_response);
        }
        catch (ArgumentException ex)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = religionDto;
            _response.Message = ex.Message;
            return BadRequest(_response);
        }
        catch (InvalidOperationException ex)
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = religionDto;
            _response.Message = ex.Message;
            return Conflict(_response);
        }
        catch (KeyNotFoundException ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = ex.Message;
            _response.Data = religionDto;
            return NotFound(_response);
        }
        catch (Exception)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar a Religião!";
            _response.Data = religionDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _religionService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Grupo de religião apagado com sucesso!";
            _response.Data = null;
            return Ok(_response);
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
            _response.Message = "Erro ao tentar apagar grupo de religião.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

}