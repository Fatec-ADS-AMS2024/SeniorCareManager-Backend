using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ProductTypeDTO
{
    public int Id { get; set; }

    [NullOrEmpty(ErrorMessage = "O campo 'Nome' não pode ser nulo ou vazio.")]
    [RemoveSpaces]
    public string Name { get; set; }

    [NumValidator(1, ErrorMessage = "É necessário informar um grupo de produto válido.")]
    public int ProductGroupId { get; set; }
}