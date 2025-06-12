using System.Reflection.Metadata;
using System.Xml.Linq;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ReligionDTO
{

    private int _id;
    private string _name;
    public int Id
    {
        get => _id;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("A Religião não pode ser nula.");
            }

        }
    }
    public string Name
    {
        get => _name;
        set
        {
            if (IsFilledString(value))
            {
                _name = value.Trim();
            }
            else
            {
                _name = null;
            }
        }
    }

    public static bool IsFilledString(params string[] parametros)
    {
        foreach (var parametro in parametros)
        {
            if (string.IsNullOrWhiteSpace(parametro))
            {
                return false;
            }
        }
        return true;
    }
}

