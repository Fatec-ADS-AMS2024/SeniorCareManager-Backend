using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Services.Interfaces;


public interface IProductGroupService: IGenericService<ProductGroup, ProductGroupDTO>
{
    Task<bool> IsDuplicateNameAsync(string name, int id = 0);
    Task<bool> CheckDuplicates(Func<ProductGroup, string?> selector, string? valor, int idIgnor);
}
