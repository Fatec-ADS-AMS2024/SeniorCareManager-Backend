using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ResidentAllergyDTO
    {

        public int Id { get; set; }

        [RemoveSpaces]
        public int ResidentId { get; set; }

        [RemoveSpaces]
        public string Description { get; set; }
    }
}
