using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{

    public class ProductDTO
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public string GenericName { get; set; }

        public decimal MinimumStock { get; set; }

        public decimal CurrentStock { get; set; }

        public decimal StockValue { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal AverageCost { get; set; }

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