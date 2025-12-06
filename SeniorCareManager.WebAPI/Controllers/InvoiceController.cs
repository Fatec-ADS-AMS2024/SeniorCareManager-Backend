using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class InvoiceController : Controller
{
    private readonly IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet, MapToApiVersion("1")]
    public async Task<IActionResult> Get([FromQuery] InvoiceFilterDTO filter)
    {
        filter ??= new InvoiceFilterDTO();
        Execute.Executar(filter);

        var (items, totalCount) = await _invoiceService.GetPagedAsync(filter);
        var payload = new
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };

        return Response<object>.Ok(payload, "Notas fiscais obtidas com sucesso!");
    }

    [HttpGet("{id:long}"), MapToApiVersion("1")]
    public async Task<IActionResult> GetById(long id)
    {
        var invoice = await _invoiceService.GetByIdAsync(id);
        return Response<InvoiceDTO>.Ok(invoice, "Nota fiscal obtida com sucesso!");
    }

    [HttpPost, MapToApiVersion("1")]
    public async Task<IActionResult> Post(InvoiceDTO invoiceDto)
    {
        ValidateInvoice(invoiceDto);
        invoiceDto.Id = 0;
        var created = await _invoiceService.CreateAsync(invoiceDto);
        return Response<InvoiceDTO>.Created(created, "Nota fiscal cadastrada com sucesso!");
    }

    [HttpPut("{id:long}"), MapToApiVersion("1")]
    public async Task<IActionResult> Put(long id, InvoiceDTO invoiceDto)
    {
        ValidateInvoice(invoiceDto);
        var updated = await _invoiceService.UpdateAsync(id, invoiceDto);
        return Response<InvoiceDTO>.Ok(updated, "Nota fiscal atualizada com sucesso!");
    }

    [HttpDelete("{id:long}"), MapToApiVersion("1")]
    public async Task<IActionResult> Delete(long id)
    {
        await _invoiceService.DeleteAsync(id);
        return Response<object>.NoContent();
    }

    private static void ValidateInvoice(InvoiceDTO invoice)
    {
        Execute.Executar(invoice);

        if (invoice.Items == null)
        {
            return;
        }

        foreach (var item in invoice.Items)
        {
            Execute.Executar(item);
        }
    }
}
