using System.ComponentModel.DataAnnotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos;

public class SupplierDTO
{
    public int Id { get; set; }

    [RequiredValidator(ErrorMessage = "Nome corporativo obrigatório.")]
    [RemoveSpaces]
    public string CorporateName { get; set; }

    [RequiredValidator(ErrorMessage = "Nome comercial obrigatório.")]
    [RemoveSpaces]
    public string TradeName { get; set; }

    [RequiredValidator(ErrorMessage = "CPF ou CNPJ obrigatório.")]
    [CpfCnpjValidator]
    [ExtractNumbers]
    [RemoveSpaces]
    public string CpfCnpj { get; set; }

    [RequiredValidator(ErrorMessage = "E-mail obrigatório.")]
    [EmailValidator(ErrorMessage = "E-mail inválido.")]
    [RemoveSpaces]
    public string Email { get; set; }

    [RequiredValidator(ErrorMessage = "Telefone obrigatório.")]
    [PhoneFormat]
    [ExtractNumbers]
    [RemoveSpaces]
    public string Phone { get; set; }

    [RequiredValidator(ErrorMessage = "CEP obrigatório.")]
    [ExtractNumbers]
    [RemoveSpaces]
    public string PostalCode { get; set; }

    [RequiredValidator(ErrorMessage = "Rua obrigatória.")]
    [RemoveSpaces]
    public string Street { get; set; }

    [RequiredValidator(ErrorMessage = "Número obrigatório.")]
    [RemoveSpaces]
    public string Number { get; set; }

    [RequiredValidator(ErrorMessage = "Bairro obrigatório.")]
    [RemoveSpaces]
    public string District { get; set; }

    [RemoveSpaces]
    public string AddressComplement { get; set; }

    [RequiredValidator(ErrorMessage = "Cidade obrigatória.")]
    [RemoveSpaces]
    public string City { get; set; }

    [RequiredValidator(ErrorMessage = "Estado obrigatório.")]
    [RemoveSpaces]
    [UpperCaracters]
    [UfValidator]
    public string State { get; set; }
}
