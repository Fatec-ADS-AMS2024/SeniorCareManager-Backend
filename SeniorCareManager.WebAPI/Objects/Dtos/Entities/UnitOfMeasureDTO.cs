using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class UnitOfMeasureDTO
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        [QtdCaractersValidator(3!, "No máximo 3 caracteres!")]
        public string Abbreviation { get; set; }
    }
}
