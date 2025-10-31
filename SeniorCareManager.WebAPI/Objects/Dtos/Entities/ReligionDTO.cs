using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ReligionDTO
{
    public int id { get; set; }
    [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
    [RemoveSpaces]
    public string Name { get; set; }
}
