using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HealthInsurancePlanController : Controller
{
    private readonly IHealthInsurancePlanService _healthInsurancePlanService;
    private readonly Response _response;

    public HealthInsurancePlanController(IHealthInsurancePlanService healthInsurancePlanService)
    {
        this._healthInsurancePlanService = healthInsurancePlanService;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var healthInsurancePlan = await _healthInsurancePlanService.GetAll();
        _response.Code = ResponseEnum.Success;
        _response.Data = healthInsurancePlan;
        _response.Message = "Lista de planos de saúde!";
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var healthInsurancePlan = await _healthInsurancePlanService.GetById(id);
            if (healthInsurancePlan is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "Plano de saúde não encontrado.";
                _response.Data = healthInsurancePlan;
                return NotFound(_response);
            }

            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde " + healthInsurancePlan.Name + " obtido com sucesso!";
            _response.Data = healthInsurancePlan;
            return Ok(_response);

        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível adquirir o plano de saúde.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }

    }

    [HttpPost]
    public async Task<IActionResult> Post(HealthInsurancePlanDTO healthInsurancePlanDto)
    {

        if (healthInsurancePlanDto is null)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "O plano é inválido.";
            return BadRequest(_response);
        }

        if (string.IsNullOrWhiteSpace(healthInsurancePlanDto.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "Nome inválido.";

            return BadRequest(_response);
        }
        if (string.IsNullOrWhiteSpace(healthInsurancePlanDto.Abbreviation))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "A abreviação do plano é obrigatória.";
            return BadRequest(_response);
        }

        var healthInsurancesPlans = await _healthInsurancePlanService.GetAll();
        if (CheckDuplicates(healthInsurancesPlans, healthInsurancePlanDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = healthInsurancePlanDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            healthInsurancePlanDto.Id = 0;
            await _healthInsurancePlanService.Create(healthInsurancePlanDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde Cadastrado com sucesso!";
            _response.Data = healthInsurancePlanDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível cadastrar o plano de saúde.";
            _response.Data = healthInsurancePlanDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        if (healthInsurancePlanDto is null)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "O plano é inválido.";
            return BadRequest(_response);
        }
        if (string.IsNullOrWhiteSpace(healthInsurancePlanDto.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = healthInsurancePlanDto;
            _response.Message = "Nome inválido.";
            return BadRequest(_response);
        }
        if (string.IsNullOrWhiteSpace(healthInsurancePlanDto.Abbreviation))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "A abreviação do plano é obrigatória.";
            return BadRequest(_response);
        }
        var healthInsurancesPlans = await _healthInsurancePlanService.GetAll();
        if (CheckDuplicates(healthInsurancesPlans, healthInsurancePlanDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = healthInsurancePlanDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            await _healthInsurancePlanService.Update(healthInsurancePlanDto, id); ;
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde alterado com sucesso!";
            _response.Data = healthInsurancePlanDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar o plano de saúde.";
            _response.Data = healthInsurancePlanDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var healthInsurancePlan = await _healthInsurancePlanService.GetById(id);
            if (healthInsurancePlan is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = null;
                _response.Message = "O Plano de saúde não foi encontrado.";
                return NotFound(_response);
            }
            await _healthInsurancePlanService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "O Plano de saúde foi apagado com sucesso!";
            _response.Data = null;
        }

        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar apagar o plano de saúde.";
            _response.Data = null;
        }
        return Ok(_response);

    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        if (healthInsurancePlanDto is null)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "O plano é inválido.";
            return BadRequest(_response);
        }
        if (string.IsNullOrWhiteSpace(healthInsurancePlanDto.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = healthInsurancePlanDto;
            _response.Message = "Nome inválido.";
            return BadRequest(_response);
        }
        if (string.IsNullOrWhiteSpace(healthInsurancePlanDto.Abbreviation))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "A abreviação do plano é obrigatória.";
            return BadRequest(_response);
        }
        var healthInsurancesPlans = await _healthInsurancePlanService.GetAll();
        if (CheckDuplicates(healthInsurancesPlans, healthInsurancePlanDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = healthInsurancePlanDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            await _healthInsurancePlanService.Update(healthInsurancePlanDto, id); ;
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde alterado com sucesso!";
            _response.Data = healthInsurancePlanDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar o plano de saúde.";
            _response.Data = healthInsurancePlanDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }
    private static bool CheckDuplicates(IEnumerable<HealthInsurancePlanDTO> healthInsurancesPlansDTO, HealthInsurancePlanDTO healthInsurancePlanDTO)
    {
        foreach (var healthInsurancePlan in healthInsurancesPlansDTO)
        {
            if (healthInsurancePlanDTO.Id == healthInsurancePlan.Id)
            {
                continue;
            }

            if (StringValidator.CompareString(healthInsurancePlanDTO.Name, healthInsurancePlan.Name))
            {
                return true;
            }
        }
        return false;
    }
}
