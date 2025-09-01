using System.ComponentModel.DataAnnotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class ManufacturerDTO
{
    public int Id { get; set; }
    [NullOrEmpty(ErrorMessage = "Nome corporativo obrigatório.")]
    public string CorporateName { get; set; }

    [NullOrEmpty(ErrorMessage = "Nome comercial obrigatório.")]
    public string TradeName { get; set; }

    [NullOrEmpty(ErrorMessage = "O CPF ou CNPJ é obrigatório.")]
    [CpfCnpjFormat]
    [ExtractNumbers]
    [RemoveSpaces]
    public string CpfCnpj { get; set; }

    [NullOrEmpty(ErrorMessage = "O telefone é obrigatório.")]
    [RemoveSpaces]
    [ExtractNumbers]
    [PhoneFormat]
    public string Phone { get; set; }

    [NullOrEmpty(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    [RemoveSpaces]
    public string Email { get; set; }
}