using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    public class ResidentAllergy
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("resident_id")]
        [ForeignKey("Resident")]
        public int ResidentId { get; set; }
        public virtual Resident? Resident { get; set; }

        [Column("allergy_id")]
        public int AllergyId { get; set; }

        [Column("detection_date")]
        public DateTime? DetectionDate { get; set; }

        [Column("released_date")]
        public DateTime? ReleasedDate { get; set; }

        public ResidentAllergy() { }

        public ResidentAllergy(int id, int allergyId, DateTime? detectionDate, DateTime? releasedDate, int residentId)
        {
            Id = id;
            ResidentId = residentId;
            AllergyId = allergyId;
            DetectionDate = detectionDate;
            ReleasedDate = releasedDate;
        }
    }
}
