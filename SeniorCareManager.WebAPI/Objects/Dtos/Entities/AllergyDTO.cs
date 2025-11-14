using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;


namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class AllergyDTO
    {
        public int Id { get; set; }

        [RequiredValidator(ErrorMessage = "O nome da alergia é obrigatório.")]
        [StringLengthValidator(100, Minimum = 2, ErrorMessage = "O nome da alergia deve ter entre 2 e 100 caracteres.")]
        public string? Name { get; set; }

        [StringLengthValidator(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RequiredValidator(ErrorMessage = "O tipo da alergia é obrigatório.")]
        [EnumValidator(typeof(AllergyType), ErrorMessage = "O tipo de alergia informado não é válido.")]
        public AllergyType Type { get; set; }
    }
}
