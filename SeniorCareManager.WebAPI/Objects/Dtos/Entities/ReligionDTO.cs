using System.Reflection.Metadata;
using System.Xml.Linq;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ReligionDTO
{
    private string _name;
    private int? _id;

    public int? Id
    {
        get => _id;
        set
        {
            _id = value;
        }
    }
    public string Name
    {
        get => _name;
        set 
        {
            _name = value.Trim();
        }
    }

    public static bool IsFilledString(params string[] parametros)
    {
        foreach (var parametro in parametros)
        {
            if (string.IsNullOrWhiteSpace(parametro))
            {
                throw new ArgumentException("O campo não pode ser nulo.");
            }
        }
        return true;
    }
    public static void IdIsValid(int? id)
    {
        if (id == null)
            throw new ArgumentNullException("O Id não pode ser nulo.");
    }
}
