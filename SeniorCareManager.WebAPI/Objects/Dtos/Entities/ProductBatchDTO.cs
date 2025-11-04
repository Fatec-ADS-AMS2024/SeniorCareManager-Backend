using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using System.ComponentModel.DataAnnotations; 
namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ProductBatchDTO
    {
        public long Id { get; set; }

        [NullOrEmpty(ErrorMessage = "Número do lote é obrigatório.")]
        [StringLength(60, ErrorMessage = "O número do lote não pode exceder 60 caracteres.")]
        [RemoveSpaces]
        public string BatchNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
        public decimal CurrentQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "O valor do estoque não pode ser negativo.")]
        public decimal CurrentStockValue { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "O ID do produto é obrigatório.")]
        public long ProductId { get; set; }
    }
}