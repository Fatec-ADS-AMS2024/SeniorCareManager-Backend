using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Controllers.Dtos;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;
    
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class AdmController : ControllerBase
{
    private IAdmService _service;

    public AdmController(IAdmService service)
    {
        _service = service;
    }

    [HttpPost("login"), MapToApiVersion("1")]
    public async Task<IActionResult> LoginAdm([FromBody] RequestAdmLoginDTO login)
    {
        Execute.Executar(login);
        return Response<object>.Ok(await _service.LoginAdm(login), "Login efetuado com êxito!");
    }

}
