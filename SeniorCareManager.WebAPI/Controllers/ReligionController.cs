using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

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
        try
        {
            var religion = await _religionService.GetAll();
            _response.Code = ResponseEnum.Success;
            _response.Data = religion;
            _response.Message = "Lista de religiões!";
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
        var religionId = await _religionService.GetById(id);
        if (religionId == null) return NotFound("Religião não encontrda!");
        return Ok(religionId);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ReligionDTO religionDto)
    {
        try
        {
            Execute.Executar(religionDto);
            religionDto.id = 0;
            await _religionService.Create(religionDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião cadastrada com sucesso!";
            _response.Data = religionDto;

            return Ok(_response);
        }
        catch (ArgumentNullException ex)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = ex.Message;
            _response.Data = religionDto;
            return NotFound(_response);
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
            Execute.Executar(religionDto);
            await _religionService.Update(religionDto, id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião alterada com sucesso!";
            _response.Data = religionDto;
            return Ok(_response);
        }
        catch (ArgumentNullException ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Message = ex.Message;
            _response.Data = religionDto;
            return NotFound(_response);
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
            _response.Message = "A religião apagado com sucesso!";
            _response.Data = null;
            return Ok(_response);
        }
        catch (ArgumentNullException ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Data = null;
            _response.Message = ex.Message;
            return NotFound(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar apagar a religião.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

}