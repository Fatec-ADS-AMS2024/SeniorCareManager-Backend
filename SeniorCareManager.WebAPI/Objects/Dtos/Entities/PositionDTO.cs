using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class PositionDTO
    {
        public int Id { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        [QtdCaractersRangeValidator(1, 50, ErrorMessage = "Quantidade de letras inválidas")]
        public string Name { get; set; }

    }
 }
