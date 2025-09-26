using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    public class ResidentAllergy
    {

            [Column("id")]
            public int Id { get; set; }

            // FK para Resident
            [Column("residentId")]
            public int ResidentId { get; set; }

            // Propriedade de navegação
            public Resident Resident { get; set; }
            [Column("description")]
            public string Description { get; set; }

            [Column("detectionDate")]
            public DateTime? DetectionDate { get; set; }

            [Column("releasedDate")]
            public DateTime? ReleasedDate { get; set; }

          
        public ResidentAllergy() { }
        public ResidentAllergy(int id, int residentId, string description, DateTime? detectionDate, DateTime? releasedDate)
        {
            Id = id;
            ResidentId = residentId;
            Description = description;
            DetectionDate = detectionDate;
            ReleasedDate = releasedDate;
        }
    }
}

