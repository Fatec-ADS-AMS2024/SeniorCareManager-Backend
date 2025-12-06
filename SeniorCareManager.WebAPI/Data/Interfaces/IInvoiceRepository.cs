using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Interfaces;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Task<(IEnumerable<Invoice> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, int? supplierId = null, StatusInvoice? status = null, DateTime? startDate = null, DateTime? endDate = null);
    Task<Invoice?> GetInvoiceWithItemsAsync(long id);
    Task<bool> ExistsByAccessKeyAsync(string accessKeyCode, long? ignoreId = null);
    Task<bool> ExistsByCompositeAsync(string invoiceNumber, string invoiceSeries, int supplierId, long? ignoreId = null);
}
