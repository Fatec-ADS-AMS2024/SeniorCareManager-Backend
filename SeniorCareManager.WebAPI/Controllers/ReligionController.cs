using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class ReligionController : Controller
{
    private readonly IReligionService _religionService;

    public ReligionController(IReligionService service)
    {
        this._religionService = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var religion = await _religionService.GetAll();
        return Ok(religion);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var religion = await _religionService.GetById(id);
        if (religion == null) return NotFound("Religião não encontrda!");
        return Ok(religion);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Religion religion)
    {
        try
        {
            await _religionService.Create(religion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir uma nova Religião.");
        }
        return Ok(religion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Religion religion)
    {
        try
        {
            await _religionService.Update(religion, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar a religião: " + ex.Message);
        }

        return Ok(religion);
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
}