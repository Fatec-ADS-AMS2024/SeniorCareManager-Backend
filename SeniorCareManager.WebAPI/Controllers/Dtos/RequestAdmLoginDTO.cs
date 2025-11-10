using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Controllers.Dtos;

public class RequestAdmLoginDTO
{
    [RequiredValidator(ErrorMessage = "Login requerido!")]
    public string Login { get; set; }

    [RequiredValidator(ErrorMessage = "Senha requerido!")]
    public string Password { get; set; }
}
