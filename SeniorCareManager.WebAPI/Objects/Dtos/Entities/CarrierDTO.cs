using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class CarrierDTO
    {
        public int Id { get; set; }

        [RequiredValidator(ErrorMessage = "A Razão Social é obrigatória.")]
        [StringLengthValidator(Maximum = 100, Minimum = 1, ErrorMessage = "A Razão Social não pode exceder 100 caracteres.")]
        public string? CorporateName { get; set; }

        [StringLengthValidator(100, ErrorMessage = "O Nome Fantasia não pode exceder 100 caracteres.")]
        public string? TradeName { get; set; }

        [ExtractNumbers]
        [RequiredValidator(ErrorMessage = "O CPF/CNPJ é obrigatório.")]
        [CpfCnpjValidator(ErrorMessage = "CPF ou CNPJ inválido.")]
        public string? CpfCnpj { get; set; }

        [RequiredValidator(ErrorMessage = "O logradouro é obrigatório.")]
        [StringLengthValidator(150, ErrorMessage = "O logradouro não pode exceder 150 caracteres.")]
        public string? Street { get; set; }

        [RequiredValidator(ErrorMessage = "O número é obrigatório.")]
        [StringLengthValidator(10, ErrorMessage = "O número não pode exceder 10 caracteres.")]
        public string? Number { get; set; }

        [RequiredValidator(ErrorMessage = "O bairro é obrigatório.")]
        [StringLengthValidator(100, ErrorMessage = "O bairro não pode exceder 100 caracteres.")]
        public string? District { get; set; }

        [StringLengthValidator(100, ErrorMessage = "O complemento não pode exceder 100 caracteres.")]
        public string? AddressComplement { get; set; }

        [RequiredValidator(ErrorMessage = "A cidade é obrigatória.")]
        [StringLengthValidator(100, ErrorMessage = "A cidade não pode exceder 100 caracteres.")]
        public string? City { get; set; }

        [UpperCaracters]
        [RequiredValidator(ErrorMessage = "O estado (UF) é obrigatório.")]
        [UfValidator(ErrorMessage = "UF inválida.")]
        public string? State { get; set; }

        [ExtractNumbers]
        [RequiredValidator(ErrorMessage = "O CEP é obrigatório.")]
        [CepValidator(ErrorMessage = "CEP inválido. Deve conter 8 dígitos.")]
        public string? PostalCode { get; set; }

        [PhoneFormat]
        [RequiredValidator(ErrorMessage = "O telefone é obrigatório.")]
        [StringLengthValidator(15, Minimum = 14, ErrorMessage = "Telefone inválido.")]
        public string? Phone { get; set; }

        [EmailValidator(ErrorMessage = "Email inválido.")]
        [StringLengthValidator(100, ErrorMessage = "O email não pode exceder 100 caracteres.")]
        public string? Email { get; set; }
    }
}
