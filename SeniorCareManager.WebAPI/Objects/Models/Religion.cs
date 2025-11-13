using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models;



[Table("religion")]
public class Religion
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [JsonIgnore]
    public ICollection<Resident>? Residents { get; set; } = new List<Resident>();

    public Religion() { }

    public Religion(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
