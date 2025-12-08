using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ResidentAllergyDTO
    {
        public int Id { get; set; }

        [StringLengthValidator(500, Minimum = 2, ErrorMessage = "A descrição deve ter entre 2 e 500 caracteres.")]
        [RemoveSpaces]
        public string? Description { get; set; }

        [DateRangeValidator(minDate: "01/01/1900")]
        public DateTime? DetectionDate { get; set; }

        [DateRangeValidator(minDate: "01/01/1900")]
        public DateTime? ReleasedDate { get; set; }

        public int ResidentId { get; set; }
        public int AllergyId { get; set; }
    }
}
