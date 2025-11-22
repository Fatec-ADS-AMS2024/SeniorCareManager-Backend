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

        [StringLengthValidator(255, ErrorMessage = "A descrição não pode exceder 255 caracteres.")]
        [RequiredValidator(ErrorMessage = "Descrição obrigatória.")]
        [RemoveSpaces]
        public string? Description { get; set; }

        [UpperCaracters]
        [StringLengthValidator(150, Minimum = 2, ErrorMessage = "O nome genérico deve ter entre 2 e 150 caracteres.")]
        [RequiredValidator(ErrorMessage = "Nome genérico obrigatório.")]
        [RemoveSpaces]
        public string? GenericName { get; set; }

        [RengeValidator(1, ErrorMessage = "Estoque mínimo tem que ser maior que zero")]
        [RequiredValidator(ErrorMessage = "Estoque mínimo obrigatório.")]
        public decimal MinimumStock { get; set; }

        [RengeValidator(0, ErrorMessage = "Quantidade atual não pode ser negativo")]
        [RequiredValidator(ErrorMessage = "Quantidade atual do estoque obrigatório.")]
        public decimal CurrentStock { get; set; }

        [RengeValidator(0, ErrorMessage = "Valor do estoque não pode ser negativo")]
        [RequiredValidator(ErrorMessage = "Valor do estoque obrigatório.")]
        public decimal StockValue { get; set; }

        [RengeValidator(0, ErrorMessage = "Preço unitário não pode ser negativo")]
        [RequiredValidator(ErrorMessage = "Preço unitário obrigatório.")]
        public decimal UnitPrice { get; set; }

        [RengeValidator(0, ErrorMessage = "Custo médio não pode ser negativo")]
        public decimal AverageCost { get; set; }

        [RengeValidator(0, ErrorMessage = "Preço da ultima compra não pode ser negativo")]
        [RequiredValidator(ErrorMessage = "Preço da última compra obrigatório.")]
        public decimal LastPurchasePrice { get; set; }

        [EnumValidator(typeof(YesNo), ErrorMessage = "Valor inválido para 'Alto Custo'.")]
        public YesNo HighCost { get; set; }

        [EnumValidator(typeof(YesNo), ErrorMessage = "Valor inválido para 'Controla Expiração'.")]
        public YesNo ExpirationControlled { get; set; }

        [RengeValidator(1, ErrorMessage = "Unidade de medida tem que ser maior que zero")]
        public int UnitOfMeasureId { get; set; }
        [RengeValidator(1, ErrorMessage = "Tipo de produto tem que ser maior que zero")]
        public int ProductTypeId { get; set; }
    }
}
