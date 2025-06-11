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
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde " + healthInsurancePlan.Name + " obtido com sucesso!";
            _response.Data = healthInsurancePlan;
            return Ok(_response);
        }
        catch (ArgumentNullException ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Message = ex.Message;
            _response.Data = null;
            return NotFound(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível adquirir o Plano de saúde.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        try
        {
            await _healthInsurancePlanService.Create(healthInsurancePlanDto); ;
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde Cadastrado com sucesso!";
            _response.Data = healthInsurancePlanDto;
        }
        catch (ArgumentNullException ex)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = ex.Message;
            _response.Data = healthInsurancePlanDto;
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
            _response.Data = healthInsurancePlanDto;
            return Conflict(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível Cadastrar o Plano de saúde.";
            _response.Data = healthInsurancePlanDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        try
        {
            await _healthInsurancePlanService.Update(healthInsurancePlanDto, id); 
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde Alterado com sucesso!";
            _response.Data = healthInsurancePlanDto;
        }
        catch (ArgumentNullException ex)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = ex.Message;
            _response.Data = healthInsurancePlanDto;
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
            _response.Data = healthInsurancePlanDto;
            return Conflict(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível Alterar o Plano de saúde.";
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
            await _healthInsurancePlanService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde apagado com sucesso!";
            _response.Data = null;
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
            _response.Message = "Erro ao tentar apagar Plano de saúde.";
            _response.Data = null;
        }
        return Ok(_response);

    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        try
        {
            await _healthInsurancePlanService.Update(healthInsurancePlanDto, id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Plano de saúde Alterado com sucesso!";
            _response.Data = healthInsurancePlanDto;
        }
        catch (ArgumentNullException ex)
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Message = ex.Message;
            _response.Data = healthInsurancePlanDto;
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
            _response.Data = healthInsurancePlanDto;
            return Conflict(_response);
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível Alterar o Plano de saúde.";
            _response.Data = healthInsurancePlanDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }
}

