using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    public class ResidentAllergy
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("detection_date")]
        public DateTime? DetectionDate { get; set; }

        [Column("released_date")]
        public DateTime? ReleasedDate { get; set; }

        [Column("resident_id")]
        [ForeignKey("Resident")]
        public int ResidentId { get; set; }

        [JsonIgnore]
        public virtual Resident? Resident { get; set; }

        [Column("allergy_id")]
        [ForeignKey("Allergy")]
        public int AllergyId { get; set; }

        [JsonIgnore]
        public virtual Allergy? Allergy { get; set; }

        public ResidentAllergy() { }

        public ResidentAllergy(int id, string description, DateTime? detectionDate, DateTime? releasedDate, int residentId, int allergyId)
        {
            Id = id;
            ResidentId = residentId;
            Description = description;
            AllergyId = allergyId;
            DetectionDate = detectionDate;
            ReleasedDate = releasedDate;
        }
    }
}
