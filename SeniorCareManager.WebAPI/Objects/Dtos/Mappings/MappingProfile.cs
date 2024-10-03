using AutoMapper;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductGroupDTO, ProductGroup>().ReverseMap();
        CreateMap<ProductGroup, ProductGroupDTO>();

        // Mapping ProductType
        CreateMap<ProductTypeDTO, ProductType>().ReverseMap();
        CreateMap<ProductType, ProductTypeDTO>();
    }
}