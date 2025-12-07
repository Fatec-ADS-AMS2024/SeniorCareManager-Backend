using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using System.Linq;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceItemRepository _invoiceItemRepository;
    private readonly IMapper _mapper;

    public InvoiceService(
        IInvoiceRepository invoiceRepository,
        IInvoiceItemRepository invoiceItemRepository,
        IMapper mapper)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _mapper = mapper;
    }

    public async Task<(IEnumerable<InvoiceDTO> Items, int TotalCount)> GetPagedAsync(InvoiceFilterDTO filter)
    {
        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize = filter.PageSize <= 0 ? 10 : filter.PageSize;

        var (entities, totalCount) = await _invoiceRepository.GetPagedAsync(
            pageNumber,
            pageSize,
            filter.SupplierId,
            filter.Status,
            filter.StartDate,
            filter.EndDate);

        return (_mapper.Map<IEnumerable<InvoiceDTO>>(entities), totalCount);
    }

    public async Task<InvoiceDTO> GetByIdAsync(long id)
    {
        var invoice = await _invoiceRepository.GetInvoiceWithItemsAsync(id);
        if (invoice is null)
        {
            throw new ExceptionNotFound($"Nota fiscal com id {id} não encontrada.");
        }

        return _mapper.Map<InvoiceDTO>(invoice);
    }

    public async Task<InvoiceDTO> CreateAsync(InvoiceDTO request)
    {
        await ValidateBusinessRulesAsync(request);

        var invoice = _mapper.Map<Invoice>(request);
        invoice.Items = _mapper.Map<List<InvoiceItem>>(request.Items);
        invoice.CreatedAt = DateTime.UtcNow;
        invoice.UpdatedAt = DateTime.UtcNow;

        var created = await _invoiceRepository.Add(invoice);
        var createdWithNavigation = await _invoiceRepository.GetInvoiceWithItemsAsync(created.Id) ?? created;
        return _mapper.Map<InvoiceDTO>(createdWithNavigation);
    }

    public async Task<InvoiceDTO> UpdateAsync(long id, InvoiceDTO request)
    {
        await ValidateBusinessRulesAsync(request, id);

        var existing = await _invoiceRepository.GetInvoiceWithItemsAsync(id);
        if (existing is null)
        {
            throw new ExceptionNotFound($"Nota fiscal com id {id} não encontrada.");
        }

        _mapper.Map(request, existing);
        existing.UpdatedAt = DateTime.UtcNow;

        await _invoiceItemRepository.RemoveByInvoiceIdAsync(id);
        existing.Items = _mapper.Map<List<InvoiceItem>>(request.Items);

        await _invoiceRepository.Update(existing);

        var updated = await _invoiceRepository.GetInvoiceWithItemsAsync(id) ?? existing;
        return _mapper.Map<InvoiceDTO>(updated);
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _invoiceRepository.GetInvoiceWithItemsAsync(id);
        if (existing is null)
        {
            throw new ExceptionNotFound($"Nota fiscal com id {id} não encontrada.");
        }

        await _invoiceRepository.Remove(existing);
    }

    private async Task ValidateBusinessRulesAsync(InvoiceDTO request, long? ignoreId = null)
    {
        if (request.Items is null || !request.Items.Any())
        {
            throw new ExceptionBadRequest("A nota fiscal deve possuir ao menos um item.");
        }

        if (request.DepartureDate.HasValue && request.DepartureDate < request.InvoiceDate)
        {
            throw new ExceptionBadRequest("A data de saída não pode ser anterior à data de emissão.");
        }

        var hasAccessKey = await _invoiceRepository.ExistsByAccessKeyAsync(request.AccessKeyCode, ignoreId);
        if (hasAccessKey)
        {
            throw new ExceptionBadRequest("Já existe uma nota fiscal com esta chave de acesso.");
        }

        var hasComposite = await _invoiceRepository.ExistsByCompositeAsync(
            request.InvoiceNumber,
            request.InvoiceSeries,
            request.SupplierId,
            ignoreId);

        if (hasComposite)
        {
            throw new ExceptionBadRequest("Já existe uma nota fiscal com este número e série para o fornecedor informado.");
        }
    }
}
