using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces;

public interface IInvoiceItemRepository : IGenericRepository<InvoiceItem>
{
    Task RemoveByInvoiceIdAsync(long invoiceId);
}
