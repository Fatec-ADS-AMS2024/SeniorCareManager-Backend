using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class InvoiceFilterDTO
{
    [RengeValidator(1, ErrorMessage = "O número da página deve ser maior que zero.")]
    public int PageNumber { get; set; } = 1;

    [RengeValidator(1, ErrorMessage = "O tamanho da página deve ser maior que zero.")]
    public int PageSize { get; set; } = 10;

    public int? SupplierId { get; set; }

    public StatusInvoice? Status { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
