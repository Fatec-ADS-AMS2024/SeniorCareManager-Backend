using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class HealthInsurancePlanDTO
{
    public int Id { get; set; }

    [EnumValidator(typeof(HealthPlanType), ErrorMessage = "Tipo de plano inválido.")]
    public HealthPlanType Type { get; set; }

    [StringLengthValidator(100, Minimum = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
    [RequiredValidator(ErrorMessage = "O nome do plano de saúde é obrigatório.")]
    [RemoveSpaces]
    public string Name { get; set; }

    [StringLengthValidator(5, Minimum = 2, ErrorMessage = "A abreviação deve ter entre 2 e 5 caracteres.")]
    [RemoveSpaces]
    [UpperCaracters]
    public string Abbreviation { get; set; }
}
