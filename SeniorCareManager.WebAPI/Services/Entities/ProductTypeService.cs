using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ProductTypeService : GenericService<ProductType>, IProductTypeSevice
{
    private readonly IProductTypeRepository _productGroupRepository;
    private readonly IMapper _mapper;

    public ProductTypeService(IProductTypeRepository repository, IMapper mapper): base(repository, mapper)
    {
        _productGroupRepository = repository;
        _mapper = mapper;
    }

}

