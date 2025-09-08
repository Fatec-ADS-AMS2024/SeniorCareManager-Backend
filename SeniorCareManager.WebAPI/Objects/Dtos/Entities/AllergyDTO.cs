using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations;


namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class AllergyDTO
    {
        public int Id { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [Required(ErrorMessage = "O nome da alergia é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome da alergia deve ter entre 2 e 100 caracteres.")]
        public string Name { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [Required(ErrorMessage = "O tipo da alergia é obrigatório.")]
        [EnumDataType(typeof(AllergyType), ErrorMessage = "O tipo de alergia informado não é válido.")]
        public AllergyType Type { get; set; }
    }
}