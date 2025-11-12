using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    [Table("productbatch")]
    public class ProductBatch
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("batch_number")]
        public string BatchNumber { get; set; }

        [Column("expiration_date")]
        public DateTime? ExpirationDate { get; set; }

        [Column("current_quantity")]
        public decimal CurrentQuantity { get; set; }

        [Column("current_stock_value")]
        public decimal CurrentStockValue { get; set; }

        [Column("product_id")]
        [ForeignKey("Product")]
        public long ProductId { get; set; }

        [Column("supplier_id")]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }

        [Column("manufacturer_id")]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        [JsonIgnore]
        public virtual Product? Product { get; set; }

        [JsonIgnore]
        public virtual Supplier? Supplier { get; set; }

        [JsonIgnore]
        public virtual Manufacturer? Manufacturer { get; set; }

        public ProductBatch()
        {
        }

        public ProductBatch(long id, string batchNumber, DateTime? expirationDate, decimal currentQuantity, decimal currentStockValue, long productId, int manufacturerId, int supplierId)
        {
            Id = id;
            BatchNumber = batchNumber;
            ExpirationDate = expirationDate;
            CurrentQuantity = currentQuantity;
            CurrentStockValue = currentStockValue;
            ProductId = productId;
            ManufacturerId = manufacturerId;
            SupplierId = supplierId;
        }
    }
}
