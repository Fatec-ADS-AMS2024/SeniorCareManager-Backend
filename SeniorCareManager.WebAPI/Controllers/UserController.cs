using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

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
        return Response<IEnumerable<UserDTO>>.Ok(users, "Lista de usuários obtida com sucesso!");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id);
        return Response<UserDTO>.Ok(user, "Usuário obtido com sucesso!");
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDTO user)
    {
        Execute.Executar(user);
        user.Id = 0;
        await _userService.Create(user);

        return Response<UserDTO>.Created(user, "Usuário cadastrado com sucesso!");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UserDTO user)
    {
        Execute.Executar(user);
        await _userService.Update(user, id);

        return Response<UserDTO>.Ok(user, "Usuário atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Remove(id);

        return Response<object>.NoContent("Usuário apagado com sucesso!");
    }
}