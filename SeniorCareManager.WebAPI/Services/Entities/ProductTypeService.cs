using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ProductTypeService : GenericService<ProductType, ProductTypeDTO>, IProductTypeService
{
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductTypeService(IProductTypeRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _productTypeRepository = repository;
        _mapper = mapper;
    }

    public async Task<bool> IsDuplicateNameAsync(string name, int id = 0)
    {
        var allTypes = await _productTypeRepository.Get();
        return StringValidator.ContainsDuplicate(
            list: allTypes,
            nameSelector: pt => pt.Name,
            currentName: name,
            currentId: id,
            idSelector: pt => pt.Id
        );
    }

    public async Task<int?> GetGroupIdIfDuplicateAsync(string name, int currentId = 0)
    {
        var allTypes = await _productTypeRepository.Get();
        var match = allTypes
            .Where(pt => pt.Id != currentId && StringValidator.CompareString(pt.Name.Trim(), name.Trim()))
            .FirstOrDefault();

        return match?.ProductGroupId;
    }

    public async Task<bool> GroupExistsAsync(int groupId)
    {
        var allTypes = await _productTypeRepository.Get();
        return allTypes.Any(x => x.ProductGroupId == groupId);
    }
}
