using System.ComponentModel.DataAnnotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class ManufacturerDTO
{
    public int Id { get; set; }

    [StringLengthValidator(150, Minimum = 5, ErrorMessage = "A Razão Social deve ter entre 5 e 150 caracteres.")] // <-- ADIÇÃO
    [RequiredValidator(ErrorMessage = "Nome corporativo obrigatório.")]
    [RemoveSpaces]
    public string? CorporateName { get; set; }

    [StringLengthValidator(100, Minimum = 2, ErrorMessage = "O Nome Fantasia deve ter entre 2 e 100 caracteres.")] // <-- ADIÇÃO
    [RequiredValidator(ErrorMessage = "Nome comercial obrigatório.")]
    [RemoveSpaces]
    public string? TradeName { get; set; }

    [CpfCnpjValidator(ErrorMessage = "CPF ou CNPJ inválido.")] // <-- ADIÇÃO
    [RequiredValidator(ErrorMessage = "O CPF ou CNPJ é obrigatório.")]
    [ExtractNumbers]
    [RemoveSpaces]
    public string? CpfCnpj { get; set; }

    [RequiredValidator(ErrorMessage = "O telefone é obrigatório.")]
    [RemoveSpaces]
    [ExtractNumbers]
    [PhoneFormat]
    public string? Phone { get; set; }

    [EmailValidator(ErrorMessage = "O e-mail informado não é válido.")]
    [StringLengthValidator(150, Minimum = 5, ErrorMessage = "O e-mail deve ter entre 5 e 150 caracteres.")]
    [RequiredValidator(ErrorMessage = "O e-mail é obrigatório.")]
    [RemoveSpaces]
    public string? Email { get; set; }
}
