using System.Reflection.Metadata;
using System.Xml.Linq;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ReligionDTO
{

    public int Id { get; set; }
    private string _name;
    public string Name
    {
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value) ? value : value.Trim();
    }

}

