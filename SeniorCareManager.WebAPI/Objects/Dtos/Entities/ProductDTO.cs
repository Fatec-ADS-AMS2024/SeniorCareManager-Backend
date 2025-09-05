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

        [NullOrEmpty(ErrorMessage = "Estoque minímo obrigatório.")]
        [RemoveSpaces]
        [NumValidator(ErrorMessage = "Estoque minimo tem que ser maior que zero")]
        public decimal MinimumStock { get; set; }

        [NullOrEmpty(ErrorMessage = "Quantidade atual do estoque obrigatório.")]
        [RemoveSpaces]
        public decimal CurrentStock { get; set; }

        [NullOrEmpty(ErrorMessage = "Valor do estoque obrigatório.")]
        [RemoveSpaces]
        public decimal StockValue { get; set; }

        [NullOrEmpty(ErrorMessage = "Preço unitário obrigatório.")]
        [RemoveSpaces]
        public decimal UnitPrice { get; set; }

        public decimal AverageCost { get; set; }

        [NullOrEmpty(ErrorMessage = "Preço da última compra obrigatório.")]
        [RemoveSpaces]
        public decimal LastPurchasePrice { get; set; }


        public YesNo HighCost { get; set; }

        public YesNo ExpirationControlled { get; set; }

        public bool CheckName()
        {
            return !string.IsNullOrWhiteSpace(GenericName) &&
                   !string.IsNullOrWhiteSpace(Description); //Verifica se o campo esta vazio, caso sm
                                                           //retorna False
        }
    }
}