using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;
public class HealthInsurancePlanDTO
{
    public int Id { get; set; }

    [NullOrEmpty(ErrorMessage = "O campo tipo não pode ser nulo ou vazio.")]
    public HealthPlanType Type { get; set; }

    [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
    [RemoveSpaces]
    public string Name { get; set; }

    [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
    [RemoveSpaces]
    [UpperCaracters]
    public string Abbreviation { get; set; }

}
