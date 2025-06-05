using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Services.Entities;
public class CarrierService : GenericService<Carrier,CarrierDTO>, ICarrierService
{
    private readonly ICarrierRepository _carrierRepository;
    private readonly IMapper _mapper;
    public CarrierService(ICarrierRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _carrierRepository = repository;
        _mapper = mapper;
    }
}
