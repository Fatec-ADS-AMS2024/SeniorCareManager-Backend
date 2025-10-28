using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Controllers.Dtos;

public class RequestAdmLoginDTO
{
    [NullOrEmpty(ErrorMessage = "Login requerido!")]
    public string Login { get; set; }

    [NullOrEmpty(ErrorMessage = "Senha requerido!")]
    public string Password { get; set; }
}
