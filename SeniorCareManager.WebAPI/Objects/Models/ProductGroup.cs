using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("productgroup")]
public class ProductGroup
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }

    public ProductGroup()
    {
        
    }

    public ProductGroup(int id, string name)
    {
        Id = id;
        Name = name;
    }
}