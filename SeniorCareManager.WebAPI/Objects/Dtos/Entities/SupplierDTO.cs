using System.ComponentModel.DataAnnotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos;
public class SupplierDTO
{
    public int Id { get; set; }

    [NullOrEmpty(ErrorMessage = "Nome corporativo obrigatório.")]
    [RemoveSpaces]
    public string CorporateName { get; set; }

    [NullOrEmpty(ErrorMessage = "Nome comercial obrigatório.")]
    [RemoveSpaces]
    public string TradeName { get; set; }

    [NullOrEmpty(ErrorMessage = "CPF ou CNPJ obrigatório.")]
    [CpfCnpjFormat]
    [ExtractNumbers]
    [RemoveSpaces]
    public string CpfCnpj { get; set; }

    [NullOrEmpty(ErrorMessage = "E-mail obrigatório.")]
    [EmailValidator(ErrorMessage = "E-mail inválido.")]
    [RemoveSpaces]
    public string Email { get; set; }

    [NullOrEmpty(ErrorMessage = "Telefone obrigatório.")]
    [PhoneFormat]
    [ExtractNumbers]
    [RemoveSpaces]
    public string Phone { get; set; }

    [NullOrEmpty(ErrorMessage = "CEP obrigatório.")]
    [ExtractNumbers]
    [RemoveSpaces]
    public string PostalCode { get; set; }

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
    [RemoveSpaces]
    [UpperCaracters]
    [UfValidator]
    public string State { get; set; }
}

