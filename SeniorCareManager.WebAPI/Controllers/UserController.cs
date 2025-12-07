using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
// [Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService service)
    {
        _userService = service;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAll(); // IEnumerable<UserDTO>
        var list = users.Select(u => new UserWithoutPasswordDTO
        {
            Id = u.Id,
            Email = u.Email,
            UserType = u.UserType,
            UserStatus = u.UserStatus,
            EmployeeId = u.EmployeeId
        });

        return Response<IEnumerable<UserWithoutPasswordDTO>>.Ok(list, "Lista de usuários obtida com sucesso!");
    }

    [HttpGet("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetById(id); // UserDTO
        var dto = new UserWithoutPasswordDTO
        {
            Id = user.Id,
            Email = user.Email,
            UserType = user.UserType,
            UserStatus = user.UserStatus,
            EmployeeId = user.EmployeeId
        };
        return Response<UserWithoutPasswordDTO>.Ok(dto, "Usuário obtido com sucesso!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(UserDTO user)
    {
        Execute.Executar(user);
        user.Id = 0;
        var created = await _userService.Create(user);
        var createdWithoutPwd = new UserWithoutPasswordDTO
        {
            Id = created.Id,
            Email = created.Email,
            UserType = created.UserType,
            UserStatus = created.UserStatus,
            EmployeeId = created.EmployeeId
        };
        return Response<UserWithoutPasswordDTO>.Created(createdWithoutPwd, "Usuário cadastrado com sucesso!");
    }

    [HttpPut("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(int id, UserDTO user)
    {
        Execute.Executar(user);
        await _userService.Update(user, id);

        var dto = new UserWithoutPasswordDTO
        {
            Id = user.Id,
            Email = user.Email,
            UserType = user.UserType,
            UserStatus = user.UserStatus,
            EmployeeId = user.EmployeeId
        };

        return Response<UserWithoutPasswordDTO>.Ok(dto, "Usuário atualizado com sucesso!");
    }

    [HttpDelete("{id}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.Remove(id);
        return Response<object>.NoContent();
    }

    // Rota para alterar senha
    [HttpPatch("{id}/password"), MapToApiVersion("1")]
    public async Task<IActionResult> ChangePassword(int id, ChangePasswordDTO dto)
    {
        Execute.Executar(dto);
        await _userService.ChangePassword(id, dto);
        return Response<object>.Ok(new { Id = id }, "Senha alterada com sucesso!");
    }
}
