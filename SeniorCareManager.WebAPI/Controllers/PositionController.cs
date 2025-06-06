using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;
using SeniorCareManager.WebAPI.Objects.Contracts;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PositionController: Controller
{
    private readonly IPositionService _positionService;
    private readonly Response _response;

    public PositionController(IPositionService service)
    {
        this._positionService = service;
        _response = new Response();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var position = await _positionService.GetAll();
        _response.Code = ResponseEnum.Success;
        _response.Data = position;
        _response.Message = "Lista de cargos!";
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var position = await _positionService.GetById(id);
            if (position is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Message = "Cargo não encontrado.";
                _response.Data = position;
                return NotFound(_response);
            }

            _response.Code = ResponseEnum.Success;
            _response.Message = "Cargo " + position.Name + " obtido com sucesso!";
            _response.Data = position;
            return Ok(_response);

        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.NotFound;
            _response.Message = "Não foi possível adquirir o cargo.";
            _response.Data = null;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(PositionDTO positionDto)
    {
        if (string.IsNullOrWhiteSpace(positionDto.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = null;
            _response.Message = "Dados inválidos.";

            return BadRequest(_response);
        }
        var positions = await _positionService.GetAll();
        if (CheckDuplicates(positions, positionDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = positionDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            positionDto.Id = 0;
            await _positionService.Create(positionDto);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Cargo Cadastrado com sucesso!";
            _response.Data = positionDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível cadastrar o cargo.";
            _response.Data = positionDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, PositionDTO positionDto)
    {
        if (string.IsNullOrWhiteSpace(positionDto.Name))
        {
            _response.Code = ResponseEnum.Invalid;
            _response.Data = positionDto;
            _response.Message = "Nome inválido.";
            return BadRequest(_response);
        }
        var positions = await _positionService.GetAll();
        if (CheckDuplicates(positions, positionDto))
        {
            _response.Code = ResponseEnum.Conflict;
            _response.Data = positionDto;
            _response.Message = "Nome duplicado.";
            return BadRequest(_response);
        }
        try
        {
            await _positionService.Update(positionDto, id); ;
            _response.Code = ResponseEnum.Success;
            _response.Message = "Cargo alterado com sucesso!";
            _response.Data = positionDto;
        }
        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Não foi possível alterar o cargo!";
            _response.Data = positionDto;
            return StatusCode(StatusCodes.Status500InternalServerError, _response);
        }
        return Ok(_response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var position = await _positionService.GetById(id);
            if (position is null)
            {
                _response.Code = ResponseEnum.NotFound;
                _response.Data = null;
                _response.Message = "O cargo não foi encontrado.";
                return NotFound(_response);
            }
            await _positionService.Remove(id);
            _response.Code = ResponseEnum.Success;
            _response.Message = "Grupo de cargo apagado com sucesso!";
            _response.Data = null;
        }

        catch (Exception ex)
        {
            _response.Code = ResponseEnum.Error;
            _response.Message = "Erro ao tentar apagar grupo de cargo.";
            _response.Data = null;
        }
        return Ok(_response);

    }
    private static bool CheckDuplicates(IEnumerable<PositionDTO> positionsDTO, PositionDTO positionDTO)
    {
        foreach (var position in positionsDTO)
        {
            if (positionDTO.Id == position.Id)
            {
                continue;
            }

            if (StringValidator.CompareString(positionDTO.Name, position.Name))
            {
                return true;
            }
        }
        return false;
    }


}