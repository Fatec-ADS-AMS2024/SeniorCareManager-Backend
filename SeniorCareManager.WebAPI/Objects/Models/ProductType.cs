using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("product_type")]
public class ProductType
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }

    [Column("product_group_id")]
    [ForeignKey("ProductGroup")]
    public int ProductGroupId { get; set; }

    [JsonIgnore]
    public virtual ProductGroup? ProductGroup { get; set; }

    [JsonIgnore]
    public virtual ICollection<Product>? Products { get; set; } = new List<Product>();

    public ProductType(){ }

    public ProductType(int id, string name, int productGroupId){
        Id = id;
        Name = name;
        ProductGroupId = productGroupId;
    }
}
