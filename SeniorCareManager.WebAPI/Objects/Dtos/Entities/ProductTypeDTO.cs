using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ProductTypeDTO
{
    public int Id { get; set; }

    [UpperCaracters]
    [StringLengthValidator(100, Minimum = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
    [RequiredValidator(ErrorMessage = "O campo 'Nome' não pode ser nulo ou vazio.")]
    [RemoveSpaces]
    public string? Name { get; set; }

    [RengeValidator(1, ErrorMessage = "É necessário informar um grupo de produto válido.")]
    public int ProductGroupId { get; set; }
}
