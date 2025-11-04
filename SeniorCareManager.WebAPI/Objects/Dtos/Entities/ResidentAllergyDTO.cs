using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ResidentAllergyDTO
    {
        public int Id { get; set; }

        public int ResidentId { get; set; }

        public int AllergyId { get; set; }
    }
}