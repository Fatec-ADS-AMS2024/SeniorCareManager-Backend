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
    private readonly IProductGroupRepository _productGroupRepository;
    private readonly IMapper _mapper;

    public ProductTypeService(
        IProductTypeRepository productTypeRepository,
        IProductGroupRepository productGroupRepository,
        IMapper mapper
    ) : base(productTypeRepository, mapper)
    {
        _productTypeRepository = productTypeRepository;
        _productGroupRepository = productGroupRepository;
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
        return await _productGroupRepository.ExistsAsync(groupId);
    }
}
