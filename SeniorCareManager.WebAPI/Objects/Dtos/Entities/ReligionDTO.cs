using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ReligionDTO
{
    public int Id { get; set; }

    [StringLengthValidator(2, Maximum = 50, ErrorMessage = "O campo deve conter entre 2 e 50 caracteres.")]
    [RequiredValidator(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
    [RemoveSpaces]
    public string Name { get; set; }
}
