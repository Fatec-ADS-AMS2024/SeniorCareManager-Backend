using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("position")]
    public class Position
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }

        [JsonIgnore]
    public ICollection<Employee>? Employees { get; set; }

    public Position(int id, string name)
    {
        Id = id;
        Name = name;
    }
}

