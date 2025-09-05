using AutoMapper.Configuration.Annotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class CarrierDTO
    {
        public int Id { get; set; }

        [NullOrEmpty(ErrorMessage = "Razão Social obrigatória")]
        public string CorporateName { get; set; }

        [NullOrEmpty(ErrorMessage = "Nome Fantasia obrigatória")]
        public string TradeName { get; set; }

        [NullOrEmpty(ErrorMessage = "CPF ou CNPJ obrigatório")]
        [CpfCnpjFormat]
        [ExtractNumbers]
        [RemoveSpaces]
        public string CpfCnpj { get; set; }

        [NullOrEmpty(ErrorMessage = "O nome da rua é obrigatório")]
        public string Street { get; set; }

        [NullOrEmpty(ErrorMessage = "Número obrigatório")]
        public string Number { get; set; }

        [NullOrEmpty(ErrorMessage = "Bairro obrigatório")]
        public string District { get; set; }

        public string AddressComplement { get; set; }

        [NullOrEmpty(ErrorMessage = "Cidade obrigatória")]
        public string City { get; set; }

        [NullOrEmpty(ErrorMessage = "Estado obrigatório")]
        public string State { get; set; }

        [NullOrEmpty(ErrorMessage = "CEP obrigatório")]
        public string PostalCode { get; set; }

        [NullOrEmpty(ErrorMessage = "Telefone obrigatório")]
        [RemoveSpaces]
        [ExtractNumbers]
        [PhoneFormat]
        public string Phone { get; set; }

        [NullOrEmpty(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        [RemoveSpaces]
        public string Email { get;  set; }
    }
}
