using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories;

public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Invoice> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, int? supplierId = null, StatusInvoice? status = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Invoices
            .AsNoTracking()
            .Include(i => i.Supplier)
            .Include(i => i.Carrier)
            .AsQueryable();

        if (supplierId.HasValue)
            query = query.Where(i => i.SupplierId == supplierId.Value);

        if (status.HasValue)
            query = query.Where(i => i.Status == status.Value);

        if (startDate.HasValue)
            query = query.Where(i => i.InvoiceDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(i => i.InvoiceDate <= endDate.Value);

        var total = await query.CountAsync();

        var items = await query
            .OrderByDescending(i => i.InvoiceDate)
            .Skip((Math.Max(pageNumber, 1) - 1) * Math.Max(pageSize, 1))
            .Take(Math.Max(pageSize, 1))
            .ToListAsync();

        return (items, total);
    }

    public async Task<Invoice?> GetInvoiceWithItemsAsync(long id)
    {
        return await _context.Invoices
            .Include(i => i.Supplier)
            .Include(i => i.Carrier)
            .Include(i => i.Items)
                .ThenInclude(ii => ii.Product)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<bool> ExistsByAccessKeyAsync(string accessKeyCode, long? ignoreId = null)
    {
        var normalized = accessKeyCode.Trim();
        return await _context.Invoices.AnyAsync(i => i.AccessKeyCode == normalized && (!ignoreId.HasValue || i.Id != ignoreId));
    }

    public async Task<bool> ExistsByCompositeAsync(string invoiceNumber, string invoiceSeries, int supplierId, long? ignoreId = null)
    {
        return await _context.Invoices.AnyAsync(i =>
            i.InvoiceNumber == invoiceNumber &&
            i.InvoiceSeries == invoiceSeries &&
            i.SupplierId == supplierId &&
            (!ignoreId.HasValue || i.Id != ignoreId));
    }
}
