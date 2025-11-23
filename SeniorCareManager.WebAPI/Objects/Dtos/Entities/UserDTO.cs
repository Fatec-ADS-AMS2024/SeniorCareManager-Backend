using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class UserDTO
{
    public int Id { get; set; }

    [RequiredValidator(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailValidator(ErrorMessage = "E-mail inválido.")]
    [StringLengthValidator(150, Minimum = 5, ErrorMessage = "O e-mail deve ter entre 5 e 150 caracteres.")]
    [RemoveSpaces]
    public string Email { get; set; } = string.Empty;

    [RequiredValidator(ErrorMessage = "A senha é obrigatória.")]
    [StringLengthValidator(255, Minimum = 6, ErrorMessage = "A senha deve ter entre 6 e 255 caracteres.")]
    public string Password { get; set; } = string.Empty;

    [RequiredValidator(ErrorMessage = "O tipo de usuário é obrigatório.")]
    [EnumValidator(typeof(UserType), ErrorMessage = "Tipo de usuário inválido.")]
    public UserType UserType { get; set; }

    [RequiredValidator(ErrorMessage = "O status do usuário é obrigatório.")]
    [EnumValidator(typeof(UserStatus), ErrorMessage = "Status do usuário inválido.")]
    public UserStatus UserStatus { get; set; }

}
