using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{

    public class ProductDTO
    {
        public long Id { get; set; }

        [NullOrEmpty(ErrorMessage = "Descrição obrigatória.")]
        [RemoveSpaces]
        public string Description { get; set; }

        [NullOrEmpty(ErrorMessage = "Nome genérico obrigatório.")]
        [RemoveSpaces]
        public string GenericName { get; set; }

        [NullOrEmpty(ErrorMessage = "Estoque mínimo obrigatório.")]
        [NumValidator(1, ErrorMessage = "Estoque mínimo tem que ser maior que zero")]
        public decimal MinimumStock { get; set; }
 
        [NullOrEmpty(ErrorMessage = "Quantidade atual do estoque obrigatório.")]
		[NumValidator(0, ErrorMessage = "Preço unitário não pode ser negativo")]
		public decimal CurrentStock { get; set; }

        [NullOrEmpty(ErrorMessage = "Valor do estoque obrigatório.")]
		[NumValidator(0, ErrorMessage = "Valor do estoque não pode ser negativo")]
		public decimal StockValue { get; set; }

        [NullOrEmpty(ErrorMessage = "Preço unitário obrigatório.")]
		[NumValidator(0, ErrorMessage = "Preço unitário não pode ser negativo")]
		public decimal UnitPrice { get; set; }

        public decimal AverageCost { get; set; }

        [NullOrEmpty(ErrorMessage = "Preço da última compra obrigatório.")]
		[NumValidator(0, ErrorMessage = "Preço da ultima compra não pode ser negativo")]
		public decimal LastPurchasePrice { get; set; }

        public YesNo HighCost { get; set; }

        public YesNo ExpirationControlled { get; set; }

    }
}