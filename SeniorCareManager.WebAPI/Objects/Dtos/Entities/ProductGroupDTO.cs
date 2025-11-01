using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ProductGroupDTO
{
    public int Id { get; set; }

    [NullOrEmpty(ErrorMessage = "O campo 'Nome' não pode ser nulo ou vazio.")]
    [RemoveSpaces]
    public string Name { get; set; }
}