using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;
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
            var religion = await _religionService.GetById(id);
            if (religion == null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "Religião não encontrada.";
                _response.Data = religion;
                return NotFound(_response);
            }

            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião " + religion.Name + " obtido com sucesso!";
            _response.Data = religion;
            return Ok(_response);

        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Message = "Não foi possível adquirir a religião.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(ReligionDTO religionDto)
    {
        var religions = await _religionService.GetAll();
        if (religions == null)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = religionDto;
            _response.Message = "Nome inválido.";
            return BadRequest(_response);
        }
        if (!CheckDuplicates(religions, religionDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = religionDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            await _religionService.Create(religionDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião Cadastrado com sucesso!";
            _response.Data = religionDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível cadastrar a Religião.";
            _response.Data = religionDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ReligionDTO religionDto)
    {
        var religions = await _religionService.GetAll();
        if (religionDto == null)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = religionDto;
            _response.Message = "Nome inválido.";
            return BadRequest(_response);
        }
        if (!CheckDuplicates(religions, religionDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = religionDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            await _religionService.Update(religionDto, id); ;
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião alterado com sucesso!";
            _response.Data = religionDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar a Religião!";
            _response.Data = religionDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
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
        }

        catch (Exception ex)
        {
            await _religionService.Remove(id);
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar apagar grupo de religião.";
            _response.Data = null;
        }
        return Ok(_response);

    }
    private static bool CheckDuplicates(IEnumerable<ReligionDTO> religionsDTO, ReligionDTO religionDTO)
    {
        foreach (var religion in religionsDTO)
        {
            if (StringValidator.CompareString(religionDTO.Name, religion.Name))
            {
                return false;
            }
        }
        return true;
    }

}