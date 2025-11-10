using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ProductBatchDTO
    {
        public long? Id { get; set; }

        [StringLengthValidator(60, ErrorMessage = "O número do lote não pode exceder 60 caracteres.")]
        [RequiredValidator(ErrorMessage = "Número do lote é obrigatório.")]
        [RemoveSpaces]
        public string? BatchNumber { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [RequiredValidator(ErrorMessage = "A quantidade é obrigatória.")]
        [RengeValidator(0, ErrorMessage = "A quantidade não pode ser negativa.")]
        public decimal CurrentQuantity { get; set; }

        [RequiredValidator(ErrorMessage = "O valor do estoque é obrigatório.")]
        [RengeValidator(0, ErrorMessage = "O valor do estoque não pode ser negativo.")]
        public decimal CurrentStockValue { get; set; }

        [RequiredValidator(ErrorMessage = "O ID do produto é obrigatório.")]
        [RengeValidator(1, ErrorMessage = "O ID do produto é obrigatório.")]
        public long ProductId { get; set; }

        [RequiredValidator(ErrorMessage = "O ID do fornecedor é obrigatório.")]
        [RengeValidator(1, ErrorMessage = "O ID do fornecedor é obrigatório.")]
        public int SupplierId { get; set; }

        [RequiredValidator(ErrorMessage = "O ID do fabricante é obrigatório.")]
        [RengeValidator(1, ErrorMessage = "O ID do fabricante é obrigatório.")]
        public int ManufacturerId { get; set; }
    }
}
