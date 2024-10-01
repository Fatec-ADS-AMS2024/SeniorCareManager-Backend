using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;


namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class UnitOfMeasureService : GenericService<UnitOfMeasure>
    {
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IMapper _mapper;

        public UnitOfMeasureService(IProductGroupRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _productGroupRepository = repository;
            _mapper = mapper;
        }
    }
}
