using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ResidentRelativeDTO
    {
        public int Id { get; set; }

        [RengeValidator(1, ErrorMessage = "O ID do residente é obrigatório.")]
        public int ResidentId { get; set; }

        [EnumValidator(typeof(Relationship), ErrorMessage = "Parentesco inválido.")]
        public Relationship Relationship { get; set; } 

        [RequiredValidator(ErrorMessage = "O nome é obrigatório.")]
        [UpperCaracters]
        [StringLengthValidator(150, Minimum = 2, ErrorMessage = "O nome deve ter entre 2 e 150 caracteres.")]
        [RemoveSpaces]
        public string Name { get; set; }

        [ExtractNumbers]
        [RequiredValidator(ErrorMessage = "O CPF é obrigatório.")]
        [StringLengthValidator(11, Minimum = 11, ErrorMessage = "CPF inválido.")]
        [RemoveSpaces]
        public string Cpf { get; set; }

        [RequiredValidator(ErrorMessage = "O RG é obrigatório.")]
        [StringLengthValidator(20, Minimum = 2, ErrorMessage = "RG inválido.")]
        [RemoveSpaces]
        public string Rg { get; set; }

        [UpperCaracters]
        [UfValidator(ErrorMessage = "UF inválida.")]
        [RequiredValidator(ErrorMessage = "O estado emissor é obrigatório.")]
        [RemoveSpaces]
        public string IssuingState { get; set; }

        [RequiredValidator(ErrorMessage = "A nacionalidade é obrigatória.")]
        [UpperCaracters]
        [StringLengthValidator(50, Minimum = 2, ErrorMessage = "A nacionalidade deve ter entre 2 e 50 caracteres.")]
        [RemoveSpaces]
        public string Citizenship { get; set; }

        [RequiredValidator(ErrorMessage = "O celular é obrigatório.")]
        [PhoneFormat]
        [ExtractNumbers]
        [RemoveSpaces]
        public string MobileNumber { get; set; }

        [PhoneFormat]
        [ExtractNumbers]
        [RemoveSpaces]
        public string HomePhoneNumber { get; set; }

        [EmailValidator(ErrorMessage = "Email inválido.")]
        [StringLengthValidator(100, ErrorMessage = "O email não pode exceder 100 caracteres.")]
        [RemoveSpaces]
        public string Email { get; set; }

        [RequiredValidator(ErrorMessage = "A rua é obrigatória.")]
        [UpperCaracters]
        [StringLengthValidator(150, ErrorMessage = "A rua não pode exceder 150 caracteres.")]
        [RemoveSpaces]
        public string Street { get; set; }

        [RequiredValidator(ErrorMessage = "O número é obrigatório.")]
        [StringLengthValidator(10, ErrorMessage = "O número não pode exceder 10 caracteres.")]
        [RemoveSpaces]
        public string Number { get; set; }

        [RequiredValidator(ErrorMessage = "O bairro é obrigatório.")]
        [UpperCaracters]
        [StringLengthValidator(100, Minimum = 2, ErrorMessage = "O bairro deve ter entre 2 e 100 caracteres.")]
        [RemoveSpaces]
        public string District { get; set; }

        [UpperCaracters]
        [StringLengthValidator(100, ErrorMessage = "O complemento não pode exceder 100 caracteres.")]
        [RemoveSpaces]
        public string AddressComplement { get; set; }

        [RequiredValidator(ErrorMessage = "A cidade é obrigatória.")]
        [UpperCaracters]
        [StringLengthValidator(100, Minimum = 2, ErrorMessage = "A cidade deve ter entre 2 e 100 caracteres.")]
        [RemoveSpaces]
        public string City { get; set; }

        [RequiredValidator(ErrorMessage = "O estado (UF) é obrigatório.")]
        [UpperCaracters]
        [UfValidator(ErrorMessage = "UF inválida.")]
        [RemoveSpaces]
        public string State { get; set; }

        [RequiredValidator(ErrorMessage = "O CEP é obrigatório.")]
        [CepValidator(ErrorMessage = "CEP inválido. Deve conter 8 dígitos.")]
        [ExtractNumbers]
        [RemoveSpaces]
        public string PostalCode { get; set; }

        [UpperCaracters]
        [StringLengthValidator(50, Minimum = 2, ErrorMessage = "Órgão emissor inválido.")]
        [RemoveSpaces]
        public string IssuingBody { get; set; }
    }
}
