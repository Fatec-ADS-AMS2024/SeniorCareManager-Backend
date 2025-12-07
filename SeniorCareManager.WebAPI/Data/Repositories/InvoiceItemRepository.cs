using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Data.Repositories;

public class InvoiceItemRepository : GenericRepository<InvoiceItem>, IInvoiceItemRepository
{
    private readonly AppDbContext _context;

    public InvoiceItemRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task RemoveByInvoiceIdAsync(long invoiceId)
    {
        var items = await _context.InvoiceItems.Where(ii => ii.InvoiceId == invoiceId).ToListAsync();
        _context.InvoiceItems.RemoveRange(items);
        await _context.SaveChangesAsync();
    }
}
