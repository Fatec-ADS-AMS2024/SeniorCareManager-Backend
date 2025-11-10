using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class PositionDTO
    {
        public int Id { get; set; }

        [StringLengthValidator(100, Minimum = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")] // <-- ADIÇÃO
        [RequiredValidator(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string? Name { get; set; }
    }
}
