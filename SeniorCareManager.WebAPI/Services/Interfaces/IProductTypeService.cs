using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Services.Interfaces;

public interface IProductTypeService : IGenericService<ProductType, ProductTypeDTO>
{
    Task<bool> IsDuplicateNameAsync(string name, int id = 0);
    Task<int?> GetGroupIdIfDuplicateAsync(string name, int currentId = 0);
    Task<bool> GroupExistsAsync(int groupId);
}
