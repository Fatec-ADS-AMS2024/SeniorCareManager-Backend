using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ChangePasswordDTO
{
    [RequiredValidator(ErrorMessage = "A senha atual é obrigatória.")]
    public string OldPassword { get; set; }

    [RequiredValidator(ErrorMessage = "A nova senha é obrigatória.")]
    [StringLengthValidator(255, Minimum = 6, ErrorMessage = "A nova senha deve ter entre 6 e 255 caracteres.")]
    public string NewPassword { get; set; }

    [RequiredValidator(ErrorMessage = "A confirmação de senha é obrigatória.")]
    [StringLengthValidator(255, Minimum = 6, ErrorMessage = "A confirmação de senha deve ter entre 6 e 255 caracteres.")]
    public string ConfirmPassword { get; set; }
}
