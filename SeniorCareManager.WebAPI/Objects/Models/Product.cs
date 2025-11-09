using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("product")]
public class Product
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("description")]
    public string Description { get; set; }

    [Column("generic_name")]
    public string GenericName { get; set; }

    [Column("minimum_stock")]
    public decimal MinimumStock { get; set; }

    [Column("current_stock")]
    public decimal CurrentStock { get; set; }

    [Column("stock_value")]
    public decimal StockValue { get; set; }

    [Column("unit_price")]
    public decimal UnitPrice { get; set; }

    [Column("average_cost")]
    public decimal AverageCost { get; set; }

    [Column("last_purchase_price")]
    public decimal LastPurchasePrice { get; set; }

    [Column("high_cost")]
    public YesNo HighCost { get; set; }

    [Column("expiration_controlled")]
    public YesNo ExpirationControlled { get; set; }

    [Column("product_type_id")]
    [ForeignKey("ProductType")]
    public int ProductTypeId { get; set; }

    [JsonIgnore]
    public virtual ProductType? ProductType { get; set; }

    [Column("unit_of_measure_id")]
    [ForeignKey("UnitOfMeasure")]
    public int UnitOfMeasureId { get; set; }

    [JsonIgnore]
    public virtual ICollection<Supplier>? ProductSuppliers { get; set; } = new List<Supplier>();

    [JsonIgnore]
    public virtual UnitOfMeasure? UnitOfMeasure { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProductBatch>? ProductBatches { get; set; }

    public Product() {}

    public Product(long id, string description, string genericName, decimal minimumStock, decimal currentStock, decimal stockValue, decimal unitPrice, decimal averageCost, decimal lastPurchasePrice, YesNo highCost, YesNo expirationControlled, int unitOfMeasureId, int productTypeId)
    {
        Id = id;
        Description = description;
        GenericName = genericName;
        MinimumStock = minimumStock;
        CurrentStock = currentStock;
        StockValue = stockValue;
        UnitPrice = unitPrice;
        AverageCost = averageCost;
        LastPurchasePrice = lastPurchasePrice;
        HighCost = highCost;
        ExpirationControlled = expirationControlled;
        UnitOfMeasureId = unitOfMeasureId;
        ProductTypeId = productTypeId;
    }
}
