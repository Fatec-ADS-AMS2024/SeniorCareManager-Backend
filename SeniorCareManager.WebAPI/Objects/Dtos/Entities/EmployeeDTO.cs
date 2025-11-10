using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [StringLengthValidator(150, Minimum = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
        [RequiredValidator(ErrorMessage = "O nome é obrigatório.")]
        [RemoveSpaces]
        public string? Name { get; set; }

        [ExtractNumbers]
        [CpfCnpjValidator(ValidationType.Cpf, ErrorMessage = "CPF inválido.")]
        [RequiredValidator(ErrorMessage = "O CPF é obrigatório.")]
        [RemoveSpaces]

        public string? Cpf { get; set; }

        [ExtractNumbers]
        [RemoveSpaces]
        [PhoneFormat]
        public string? Phone { get; set; }

        [EmailValidator]
        [RemoveSpaces]
        public string? Email { get; set; }

        [DateRangeValidator(minDate:"01/01/1900")]
        [RequiredValidator(ErrorMessage = "A data de contratação é obrigatória.")]
        public DateTime HireDate { get; set; }

        [UpperCaracters]
        [UfValidator]
        [RequiredValidator(ErrorMessage = "O estado (UF) é obrigatório.")]
        [RemoveSpaces]

        public string? State { get; set; }

        [StringLengthValidator(100, Minimum = 2, ErrorMessage = "A cidade deve ter entre 2 e 100 caracteres.")]
        [RequiredValidator(ErrorMessage = "A cidade é obrigatória.")]
        [RemoveSpaces]
        public string? City { get; set; }

        [StringLengthValidator(150, ErrorMessage = "A rua não pode exceder 150 caracteres.")]
        [RequiredValidator(ErrorMessage = "A rua é obrigatória.")]
        [RemoveSpaces]
        public string? Street { get; set; }

        [ExtractNumbers]
        [CepValidator(ErrorMessage = "CEP inválido. Deve conter 8 dígitos.")]
        [RequiredValidator(ErrorMessage = "O CEP é obrigatório.")]
        [RemoveSpaces]
        public string? Cep { get; set; }

        [StringLengthValidator(10, ErrorMessage = "O número não pode exceder 10 caracteres.")]
        [RequiredValidator(ErrorMessage = "O número é obrigatório.")]
        public string? Number { get; set; }

        [StringLengthValidator(100, ErrorMessage = "O bairro não pode exceder 100 caracteres.")]
        [RequiredValidator(ErrorMessage = "O bairro é obrigatório.")]
        [RemoveSpaces]
        public string? Neighborhood { get; set; }

        [EnumValidator(typeof(StatusEmployee), ErrorMessage = "Status inválido.")]
        public StatusEmployee StatusEmployee { get; set; }
        [RengeValidator(1, ErrorMessage = "O cargo é obrigatório.")]
        [RequiredValidator(ErrorMessage = "O cargo é obrigatório.")]
        public int PositionId { get; set; }
    }
}
