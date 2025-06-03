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

        if (religion == null)
        {
            return StatusCode(500, $"Nenhuma religião encontrada!");
        }

        else
            return Ok(religion);
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
        var religions = await _religionService.GetAll();
        if (!CheckDuplicates(religions, religionDto))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = religionDto;
            _response.Message = "Nome duplicado ou inválido";
            return BadRequest(_response);
        }
        try
        {
            await _religionService.Create(religionDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião Cadastrado com sucesso!";
            _response.Data = null;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível cadastrar a Religião!";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ReligionDTO religionDto)
    {
        var religions = await _religionService.GetAll();
        if (!CheckDuplicates(religions, religionDto))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = religionDto;
            _response.Message = "Nome duplicado ou inválido";
            return BadRequest(_response);
        }
        try
        {
            await _religionService.Create(religionDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Religião alterado com sucesso!";
            _response.Data = null;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar a Religião!";
            _response.Data = null;
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
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar remover a religião.");
        }

        return Ok("Grupo de religião apagado com sucesso");
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