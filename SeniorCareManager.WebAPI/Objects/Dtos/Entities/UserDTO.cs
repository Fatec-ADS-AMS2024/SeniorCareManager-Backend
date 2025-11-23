using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class UserDTO
{
    public int Id { get; set; }

    [RequiredValidator(ErrorMessage = "E-mail obrigatório.")]
    [EmailValidator(ErrorMessage = "E-mail inválido.")]
    [RemoveSpaces]
    public string Email { get; set; }

    [RequiredValidator(ErrorMessage = "Senha obrigatória.")]
    public string Password { get; set; }

    [RequiredValidator(ErrorMessage = "O campo 'Tipo de usuário' não pode ser nulo ou vazio.")]
    public UserType UserType { get; set; }

    [RequiredValidator(ErrorMessage = "O campo 'Status do usuário' não pode ser nulo ou vazio.")]
    public UserStatus UserStatus { get; set; }
}
