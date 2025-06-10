using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;
public class ProductGroupService : GenericService<ProductGroup, ProductGroupDTO>, IProductGroupService
{
    private readonly IProductGroupRepository _productGroupRepository;
    private readonly IMapper _mapper;

    public ProductGroupService(IProductGroupRepository repository, IMapper mapper)
        : base(repository, mapper)
    {
        _productGroupRepository = repository;
        _mapper = mapper;
    }
    public async Task<bool> IsDuplicateNameAsync(string name, int id = 0)
    {
        var allGroups = await _productGroupRepository.Get();
        return StringValidator.ContainsDuplicate(
            list: allGroups,
            nameSelector: g => g.Name,
            currentName: name,
            currentId: id,
            idSelector: g => g.Id
        );
    }
}