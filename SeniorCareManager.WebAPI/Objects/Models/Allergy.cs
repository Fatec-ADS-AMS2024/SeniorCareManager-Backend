using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    [Table("allergy")]
    public class Allergy
    {

        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("type")]
        public AllergyType Type { get; set; }

        [JsonIgnore]
        public virtual ICollection<ResidentAllergy> Residents { get; set; } = new List<ResidentAllergy>();

        public Allergy()
        {

        }

        public Allergy(int id, string name, AllergyType type)
        {
            Id = id;
            Name = name;
            Type = type;
        }

    }
}
