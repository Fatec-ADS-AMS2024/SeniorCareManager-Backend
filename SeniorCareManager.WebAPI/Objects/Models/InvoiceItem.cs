using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("invoice_item")]
public class InvoiceItem
{
    [Column("id")]
    public long Id { get; set; }

    [Column("invoice_id")]
    public long InvoiceId { get; set; }

    public Invoice? Invoice { get; set; }

    [Column("product_id")]
    public long ProductId { get; set; }

    public Product? Product { get; set; }

    [Column("cfop_code")]
    public string? CfopCode { get; set; }

    [Column("csosn_code")]
    public string? CsosnCode { get; set; }

    [Column("quantity")]
    public decimal Quantity { get; set; }

    [Column("unit_price")]
    public decimal UnitPrice { get; set; }

    [Column("unit_cost")]
    public decimal UnitCost { get; set; }

    [Column("total_price")]
    public decimal TotalPrice { get; set; }

    [Column("transportation_allocation")]
    public decimal TransportationAllocation { get; set; }

    [Column("insurance_allocation")]
    public decimal InsuranceAllocation { get; set; }

    [Column("other_charges_allocation")]
    public decimal OtherChargesAllocation { get; set; }

    [Column("discount_allocation")]
    public decimal DiscountAllocation { get; set; }

    [Column("icms_rate")]
    public decimal ICMSRate { get; set; }

    [Column("icms_base_value")]
    public decimal ICMSBaseValue { get; set; }

    [Column("icms_value")]
    public decimal ICMSValue { get; set; }

    [Column("icms_sub_base_value")]
    public decimal ICMSSubBaseValue { get; set; }

    [Column("icms_sub_value")]
    public decimal ICMSSubValue { get; set; }

    [Column("ipi_rate")]
    public decimal IPIRate { get; set; }

    [Column("ipi_base_value")]
    public decimal IPIBaseValue { get; set; }

    [Column("ipi_value")]
    public decimal IPIValue { get; set; }
}
