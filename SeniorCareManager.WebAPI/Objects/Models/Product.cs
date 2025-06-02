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

    [Required]
    [Column("description", TypeName = "varchar(100)")]
    public string Description { get; set; }

    [Required]
    [Column("genericname", TypeName = "varchar(100)")]
    public string GenericName { get; set; }

    [Column("minimum_stock", TypeName = "decimal(10,2)")]
    public decimal MinimumStock { get; set; }

    [Column("current_stock", TypeName = "decimal(10,2)")]
    public decimal CurrentStock { get; set; }

    [Column("stock_value", TypeName = "decimal(10,2)")]
    public decimal StockValue { get; set; }

    [Column("unit_price", TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    [Column("average_cost", TypeName = "decimal(10,2)")]
    public decimal AverageCost { get; set; }

    [Column("last_purchase_price", TypeName = "decimal(10,2)")]
    public decimal LastPurchasePrice { get; set; }

    [Column("high_cost")]
    public YesNo HighCost { get; set; }

    [Column("expiration_controlled")]
    public YesNo ExpirationControlled { get; set; }

    public Product()
    {
        
    }

    public Product(long id, string description, string genericName, decimal minimumStock, decimal currentStock, decimal stockValue, decimal unitPrice, decimal averageCost, decimal lastPurchasePrice, YesNo highCost, YesNo expirationControlled)
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
    }
}