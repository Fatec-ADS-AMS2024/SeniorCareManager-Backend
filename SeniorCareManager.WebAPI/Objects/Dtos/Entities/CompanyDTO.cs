using System.ComponentModel.DataAnnotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        [NullOrEmpty(ErrorMessage = "Nome corporativo obrigatório.")]
        [RemoveSpaces]
        public string CompanyName { get; set; }

        [NullOrEmpty(ErrorMessage = "Nome comercial obrigatório.")]
        [RemoveSpaces]
        public string TradeName { get; set; }

        [NullOrEmpty(ErrorMessage = "O CNPJ é obrigatório.")]
        [CpfCnpjFormat]
        [ExtractNumbers]
        [RemoveSpaces]
        public string CNPJ { get; set; }

        [NullOrEmpty(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [RemoveSpaces]
        public string Email { get; set; }

        [NullOrEmpty(ErrorMessage = "Rua obrigatória.")]
        [RemoveSpaces]
        public string Street { get; set; }

        [NullOrEmpty(ErrorMessage = "Número obrigatório.")]
        [RemoveSpaces]
        public string Number { get; set; }

        [NullOrEmpty(ErrorMessage = "Bairro obrigatório.")]
        [RemoveSpaces]
        public string District { get; set; }

        [RemoveSpaces]
        public string AddressComplement { get; set; }

        [NullOrEmpty(ErrorMessage = "Cidade obrigatória.")]
        [RemoveSpaces]
        public string City { get; set; }

        [NullOrEmpty(ErrorMessage = "Estado obrigatório.")]
        [UfValidator]
        [RemoveSpaces]
        [UpperCaracters]
        public string State { get; set; }

        [NullOrEmpty(ErrorMessage = "CEP obrigatório.")]
        [ExtractNumbers]
        [RemoveSpaces]
        public string PostalCode { get; set; }

        public byte[] CompanyLogo { get; set; }
    }
}
