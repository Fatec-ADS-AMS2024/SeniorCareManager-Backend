using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;
public class HealthInsurancePlanDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public string Abbreviation { get; set; }
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
