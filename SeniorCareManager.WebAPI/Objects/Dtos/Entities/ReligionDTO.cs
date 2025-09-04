namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ReligionDTO
{
    public int id { get; set; }
    private string _name;
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
}
