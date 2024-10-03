using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("producttype")]
public class ProductType
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("producttypeid")]
    public int ProductGroupId { get; set; }

    [JsonIgnore][ForeignKey("productgroupid")]
    public ProductGroup ProductGroup { get; set; }
    public ProductType(){ 
    
    }
    public ProductType(int id, string name, int producttypeid)
    {
        Id = id;
        Name = name;
        ProductGroupId = producttypeid;
    }
}