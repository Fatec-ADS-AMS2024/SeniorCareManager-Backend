using System.ComponentModel.DataAnnotations.Schema;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("invoice")]
public class Invoice
{
    [Column("id")]
    public long Id { get; set; }

    [Column("supplier_id")]
    public int SupplierId { get; set; }

    public Supplier? Supplier { get; set; }

    [Column("carrier_id")]
    public int CarrierId { get; set; }

    public Carrier? Carrier { get; set; }

    [Column("invoice_number")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Column("invoice_series")]
    public string InvoiceSeries { get; set; } = string.Empty;

    [Column("access_key_code")]
    public string AccessKeyCode { get; set; } = string.Empty;

    [Column("invoice_date")]
    public DateTime InvoiceDate { get; set; }

    [Column("departure_date")]
    public DateTime? DepartureDate { get; set; }

    [Column("pdf_danfe")]
    public string? PdfDanfe { get; set; }

    [Column("status")]
    public StatusInvoice Status { get; set; } = StatusInvoice.OPEN;

    [Column("discount_value")]
    public decimal DiscountValue { get; set; } = 0m;

    [Column("freight_value")]
    public decimal FreightValue { get; set; } = 0m;

    [Column("total_product_amount")]
    public decimal TotalProductAmount { get; set; } = 0m;

    [Column("total_service_amount")]
    public decimal TotalServiceAmount { get; set; } = 0m;

    [Column("total_invoice_amount")]
    public decimal TotalInvoiceAmount { get; set; } = 0m;

    [Column("insurance_value")]
    public decimal InsuranceValue { get; set; } = 0m;

    [Column("ipi_value")]
    public decimal IPIValue { get; set; } = 0m;

    [Column("icms_base_value")]
    public decimal ICMSBaseValue { get; set; } = 0m;

    [Column("icms_value")]
    public decimal ICMSValue { get; set; } = 0m;

    [Column("icms_sub_base_value")]
    public decimal ICMSSubBaseValue { get; set; } = 0m;

    [Column("icms_sub_value")]
    public decimal ICMSSubValue { get; set; } = 0m;

    [Column("fcpst_value")]
    public decimal FCPSTValue { get; set; } = 0m;

    [Column("issqn_base_value")]
    public decimal ISSQNBaseValue { get; set; } = 0m;

    [Column("issqn_value")]
    public decimal ISSQNValue { get; set; } = 0m;

    [Column("other_charges_value")]
    public decimal OtherChargesValue { get; set; } = 0m;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
}
