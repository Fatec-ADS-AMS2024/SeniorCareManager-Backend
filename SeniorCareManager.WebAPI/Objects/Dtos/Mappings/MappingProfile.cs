using AutoMapper;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductGroupDTO, ProductGroup>().ReverseMap();
        CreateMap<ProductGroup, ProductGroupDTO>();
        CreateMap<Carrier, CarrierDTO>();
    }
}