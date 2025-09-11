using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService service)
    {
        this._userService = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        if (user == null) return NotFound("Usuário não encontrado!");
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(UserDTO user)
    {
        try{
            await _userService.Create(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar inserir um novo usuário.");
        }
        return Ok(user);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UserDTO user)
    {
        try
        {
            await _userService.Update(user, id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocorreu um erro ao tentar atualizar o usuário: "+ex.Message);
        }
        
        return Ok(user);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _userService.Remove(id);
        }
        catch (Exception ex)
        {  
            return StatusCode(500, "Ocorreu um erro ao tentar remover o usuário.");
        }

        return Ok("Usuário apagado com sucesso");
    }
}