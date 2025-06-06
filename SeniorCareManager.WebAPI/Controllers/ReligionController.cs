using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
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
            if (religion is null)
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
        if (string.IsNullOrWhiteSpace(religionDto.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "Dados inválidos.";

            return BadRequest(_response);
        }
        var religions = await _religionService.GetAll();
        if (CheckDuplicates(religions, religionDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = religionDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            religionDto.Id = 0;
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
        if (string.IsNullOrWhiteSpace(religionDto.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = religionDto;
            _response.Message = "Nome inválido.";
            return BadRequest(_response);
        }
        var religions = await _religionService.GetAll();
        if (CheckDuplicates(religions, religionDto))
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
            var religion = await _religionService.GetById(id);
            if (religion is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = null;
                _response.Message = "A religião não foi encontrada.";
                return NotFound(_response);
            }
            await _religionService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Grupo de religião apagado com sucesso!";
            _response.Data = null;
        }

        catch (Exception ex)
        {
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
            if (religionDTO.Id == religion.Id)
            {
                continue;
            }

            if (StringValidator.CompareString(religionDTO.Name, religion.Name))
            {
                return true;
            }
        }
        return false;
    }

}