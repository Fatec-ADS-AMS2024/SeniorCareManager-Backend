using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;
public class HealthInsurancePlanDTO
{
    public int Id { get; set; }
    public int Type { get; set; }
    private string _name;
    public string Name
    {
        get => _name;
        set => _name = value.Trim();
    }
    private string _abbreviation;
    public string Abbreviation
    {
        get => _abbreviation;
        set => _abbreviation = value.Trim();
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
    public bool EnumValid()
    {
        if (!Enum.IsDefined(typeof(HealthPlanType), Type))
            return false;
        return true; 
    }
}
