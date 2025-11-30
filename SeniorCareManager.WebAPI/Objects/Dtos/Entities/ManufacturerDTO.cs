using System.ComponentModel.DataAnnotations;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class ManufacturerDTO
{
    public int Id { get; set; }

    [NullOrEmpty(ErrorMessage = "Nome corporativo obrigatório.")]
    [StringLength(150, ErrorMessage = "Nome corporativo deve ter no máximo 150 caracteres.")]
    public string CorporateName { get; set; }

    [NullOrEmpty(ErrorMessage = "Nome comercial obrigatório.")]
    [StringLength(150, ErrorMessage = "Nome comercial deve ter no máximo 150 caracteres.")]
    public string TradeName { get; set; }

    [CpfCnpjValidator(ErrorMessage = "CPF ou CNPJ inválido.")] // <-- ADIÇÃO
    [RequiredValidator(ErrorMessage = "O CPF ou CNPJ é obrigatório.")]
    [ExtractNumbers]
    [RemoveSpaces]
    [StringLength(18, ErrorMessage = "CPF/CNPJ deve ter no máximo 18 caracteres (formatado).")]
    public string CpfCnpj { get; set; }

    [RequiredValidator(ErrorMessage = "O telefone é obrigatório.")]
    [RemoveSpaces]
    [ExtractNumbers]
    [PhoneFormat]
    [StringLength(15, ErrorMessage = "Telefone deve ter no máximo 15 caracteres.")]
    public string Phone { get; set; }

    [EmailValidator(ErrorMessage = "O e-mail informado não é válido.")]
    [StringLengthValidator(150, Minimum = 5, ErrorMessage = "O e-mail deve ter entre 5 e 150 caracteres.")]
    [RequiredValidator(ErrorMessage = "O e-mail é obrigatório.")]
    [RemoveSpaces]
    [StringLength(100, ErrorMessage = "O e-mail deve ter no máximo 100 caracteres.")]
    public string Email { get; set; }
}
