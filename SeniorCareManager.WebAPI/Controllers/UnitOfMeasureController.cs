﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UnitOfMeasureController : Controller
    {
       private readonly IUnitOfMeasureService _unitOfMeasureService;

        public UnitOfMeasureController(IUnitOfMeasureService service)
        {
            this._unitOfMeasureService = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var unitOfMeasure = await _unitOfMeasureService.GetAll();
            return Ok(unitOfMeasure);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var unitOfMeasure = await _unitOfMeasureService.GetById(id);
            if (unitOfMeasure == null) return NotFound("Unidade de medida não encontrado!");
            return Ok(unitOfMeasure);
        }

        [HttpPost]
        public async Task<IActionResult> Post(UnitOfMeasure unitOfMeasure)
        {
            try
            {
                await _unitOfMeasureService.Create(unitOfMeasure);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar inserir uma nova unidade de medida.");
            }
            return Ok(unitOfMeasure);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UnitOfMeasure unitOfMeasure)
        {
            try
            {
                await _unitOfMeasureService.Update(unitOfMeasure, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar atualizar a unidade de medida: " + ex.Message);
            }

            return Ok(unitOfMeasure);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfMeasureService.Remove(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro ao tentar remover a unidade de medida.");
            }

            return Ok("Unidade de medida apagada com sucesso");
        }
    }
}
