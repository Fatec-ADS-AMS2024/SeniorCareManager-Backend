using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class InvoiceDTO
{
    public long Id { get; set; }

    [RequiredValidator(ErrorMessage = "O fornecedor é obrigatório.")]
    [RengeValidator(1, ErrorMessage = "O fornecedor é obrigatório.")]
    public int SupplierId { get; set; }

    [RequiredValidator(ErrorMessage = "A transportadora é obrigatória.")]
    [RengeValidator(1, ErrorMessage = "A transportadora é obrigatória.")]
    public int CarrierId { get; set; }

    [RequiredValidator(ErrorMessage = "O número da nota é obrigatório.")]
    [StringLengthValidator(20, Minimum = 1, ErrorMessage = "O número da nota deve ter entre 1 e 20 caracteres.")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [RequiredValidator(ErrorMessage = "A série da nota é obrigatória.")]
    [StringLengthValidator(5, Minimum = 1, ErrorMessage = "A série da nota deve ter entre 1 e 5 caracteres.")]
    public string InvoiceSeries { get; set; } = string.Empty;

    [RequiredValidator(ErrorMessage = "A chave de acesso é obrigatória.")]
    [StringLengthValidator(44, Minimum = 44, ErrorMessage = "A chave de acesso deve conter 44 caracteres.")]
    public string AccessKeyCode { get; set; } = string.Empty;

    [RequiredValidator(ErrorMessage = "A data de emissão é obrigatória.")]
    public DateTime InvoiceDate { get; set; }

    public DateTime? DepartureDate { get; set; }

    [StringLengthValidator(500, ErrorMessage = "O link do DANFE não pode exceder 500 caracteres.")]
    public string? PdfDanfe { get; set; }

    public StatusInvoice Status { get; set; } = StatusInvoice.DRAFT;

    [RengeValidator(0, ErrorMessage = "O valor do desconto não pode ser negativo.")]
    public decimal DiscountValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor do frete não pode ser negativo.")]
    public decimal FreightValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O total de produtos não pode ser negativo.")]
    public decimal TotalProductAmount { get; set; }

    [RengeValidator(0, ErrorMessage = "O total de serviços não pode ser negativo.")]
    public decimal TotalServiceAmount { get; set; }

    [RengeValidator(0, ErrorMessage = "O total da nota não pode ser negativo.")]
    public decimal TotalInvoiceAmount { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor do seguro não pode ser negativo.")]
    public decimal InsuranceValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor do IPI não pode ser negativo.")]
    public decimal IPIValue { get; set; }

    [RengeValidator(0, ErrorMessage = "A base de ICMS não pode ser negativa.")]
    public decimal ICMSBaseValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor de ICMS não pode ser negativo.")]
    public decimal ICMSValue { get; set; }

    [RengeValidator(0, ErrorMessage = "A base de ICMS ST não pode ser negativa.")]
    public decimal ICMSSubBaseValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor de ICMS ST não pode ser negativo.")]
    public decimal ICMSSubValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O FCP-ST não pode ser negativo.")]
    public decimal FCPSTValue { get; set; }

    [RengeValidator(0, ErrorMessage = "A base do ISSQN não pode ser negativa.")]
    public decimal ISSQNBaseValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor do ISSQN não pode ser negativo.")]
    public decimal ISSQNValue { get; set; }

    [RengeValidator(0, ErrorMessage = "Outros acréscimos não podem ser negativos.")]
    public decimal OtherChargesValue { get; set; }

    public ICollection<InvoiceItemDTO> Items { get; set; } = new List<InvoiceItemDTO>();
}
