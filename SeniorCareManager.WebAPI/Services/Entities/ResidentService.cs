using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class ResidentService : GenericService<Resident, ResidentDTO>, IResidentService
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IMapper _mapper;
        public ResidentService(IResidentRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _residentRepository = repository;
            _mapper = mapper;
        }
    }
}
