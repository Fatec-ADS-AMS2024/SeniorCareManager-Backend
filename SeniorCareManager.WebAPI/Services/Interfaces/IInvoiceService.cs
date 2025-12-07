using SeniorCareManager.WebAPI.Objects.Dtos.Entities;

namespace SeniorCareManager.WebAPI.Services.Interfaces;

public interface IInvoiceService
{
    Task<(IEnumerable<InvoiceDTO> Items, int TotalCount)> GetPagedAsync(InvoiceFilterDTO filter);
    Task<InvoiceDTO> GetByIdAsync(long id);
    Task<InvoiceDTO> CreateAsync(InvoiceDTO request);
    Task<InvoiceDTO> UpdateAsync(long id, InvoiceDTO request);
    Task DeleteAsync(long id);
}
