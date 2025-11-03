using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    public class ResidentAllergy
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("residentId")]
        public int ResidentId { get; set; }
        public Resident Resident { get; set; } = null;

        [Column("allergyId")]
        public int AllergyId { get; set; }

        [Column("detectionDate")]
        public DateTime? DetectionDate { get; set; }

        [Column("releasedDate")]
        public DateTime? ReleasedDate { get; set; }

        public ResidentAllergy() { }

        public ResidentAllergy(int id, int residentId, int allergyId, DateTime? detectionDate, DateTime? releasedDate)
        {
            Id = id;
            ResidentId = residentId;
            AllergyId = allergyId;
            DetectionDate = detectionDate;
            ReleasedDate = releasedDate;
        }
    }
}