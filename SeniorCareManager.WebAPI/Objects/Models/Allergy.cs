using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
