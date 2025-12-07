using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class InvoiceItemDTO
{
    public long? Id { get; set; }

    [RequiredValidator(ErrorMessage = "O produto é obrigatório.")]
    [RengeValidator(1, ErrorMessage = "O produto é obrigatório.")]
    public long ProductId { get; set; }

    [StringLengthValidator(10, ErrorMessage = "O CFOP não pode exceder 10 caracteres.")]
    public string? CfopCode { get; set; }

    [StringLengthValidator(10, ErrorMessage = "O CSOSN não pode exceder 10 caracteres.")]
    public string? CsosnCode { get; set; }

    [RengeValidator(0, ErrorMessage = "A quantidade não pode ser negativa.")]
    public decimal Quantity { get; set; }

    [RengeValidator(0, ErrorMessage = "O preço unitário não pode ser negativo.")]
    public decimal UnitPrice { get; set; }

    [RengeValidator(0, ErrorMessage = "O custo unitário não pode ser negativo.")]
    public decimal UnitCost { get; set; }

    [RengeValidator(0, ErrorMessage = "O preço total não pode ser negativo.")]
    public decimal TotalPrice { get; set; }

    [RengeValidator(0, ErrorMessage = "O rateio de transporte não pode ser negativo.")]
    public decimal TransportationAllocation { get; set; }

    [RengeValidator(0, ErrorMessage = "O rateio de seguro não pode ser negativo.")]
    public decimal InsuranceAllocation { get; set; }

    [RengeValidator(0, ErrorMessage = "Outros rateios não podem ser negativos.")]
    public decimal OtherChargesAllocation { get; set; }

    [RengeValidator(0, ErrorMessage = "O desconto do item não pode ser negativo.")]
    public decimal DiscountAllocation { get; set; }

    [RengeValidator(0, ErrorMessage = "A alíquota de ICMS não pode ser negativa.")]
    public decimal ICMSRate { get; set; }

    [RengeValidator(0, ErrorMessage = "A base de ICMS não pode ser negativa.")]
    public decimal ICMSBaseValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor de ICMS não pode ser negativo.")]
    public decimal ICMSValue { get; set; }

    [RengeValidator(0, ErrorMessage = "A base de ICMS ST não pode ser negativa.")]
    public decimal ICMSSubBaseValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor de ICMS ST não pode ser negativo.")]
    public decimal ICMSSubValue { get; set; }

    [RengeValidator(0, ErrorMessage = "A alíquota de IPI não pode ser negativa.")]
    public decimal IPIRate { get; set; }

    [RengeValidator(0, ErrorMessage = "A base de IPI não pode ser negativa.")]
    public decimal IPIBaseValue { get; set; }

    [RengeValidator(0, ErrorMessage = "O valor de IPI não pode ser negativo.")]
    public decimal IPIValue { get; set; }
}
