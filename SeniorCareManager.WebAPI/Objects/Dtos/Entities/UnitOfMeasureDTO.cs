using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using System.ComponentModel.DataAnnotations;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class UnitOfMeasureDTO
    {
        public int Id { get; set; }

        [RequiredValidator(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        public string Description { get; set; }

        [RequiredValidator(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        [MaxLength(3, ErrorMessage = "Não pode haver mais que 3 caracteres!")]
        [UpperCaracters]
        public string Abbreviation { get; set; }
    }
}
