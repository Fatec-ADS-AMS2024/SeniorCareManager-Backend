using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models;

[Table("healthinsuranceplan")]
public class HealthInsurancePlan
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("type")]
    public HealthPlanType Type { get; set; }

    [Column("abbreviation")]
    public string Abbreviation { get; set; }
}

