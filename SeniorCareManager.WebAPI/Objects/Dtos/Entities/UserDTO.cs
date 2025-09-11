using System.ComponentModel.DataAnnotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class UserDTO
{
    public int Id { get; set; }

    [NullOrEmpty(ErrorMessage = "E-mail obrigatório.")]
    [EmailValidator(ErrorMessage = "E-mail inválido.")]
    [RemoveSpaces]
    public string Email { get; set; }

    [NullOrEmpty(ErrorMessage = "Senha obrigatória.")]
    [RemoveSpaces]
    public string Password { get; set; }

    public UserType UserType { get; set; }
    public UserStatus UserStatus { get; set; }
}