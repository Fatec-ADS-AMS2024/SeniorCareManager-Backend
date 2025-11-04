using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime ExpirationDate { get; set; }

        [Column("current_quantity")]
        public decimal CurrentQuantity { get; set; }

        [Column("current_stock_value")]
        public decimal CurrentStockValue { get; set; }

        [Column("product_id")]
        public long ProductId { get; set; }

        public Product Product { get; set; }

        public ProductBatch()
        {
        }

        public ProductBatch(long id, string batchNumber, DateTime expirationDate, decimal currentQuantity, decimal currentStockValue, long productId)
        {
            Id = id;
            BatchNumber = batchNumber;
            ExpirationDate = expirationDate;
            CurrentQuantity = currentQuantity;
            CurrentStockValue = currentStockValue;
            ProductId = productId;
        }
    }
}
