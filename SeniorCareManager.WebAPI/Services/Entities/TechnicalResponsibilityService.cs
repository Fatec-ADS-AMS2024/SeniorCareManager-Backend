using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class TechnicalResponsibilityService : GenericService<TechnicalResponsibility, TechnicalResponsibilityDTO>, ITechnicalResponsibilityService
    {
        private readonly ITechnicalResponsibilityRepository _technicalResponsibilityRepository;
        private readonly IMapper _mapper;

        public TechnicalResponsibilityService(ITechnicalResponsibilityRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _technicalResponsibilityRepository = repository;
            _mapper = mapper;
        }
        public override async Task<TechnicalResponsibilityDTO> GetById(int id)
        {
            var errors = new List<FieldError>();
            var technicalResponsibility = await _technicalResponsibilityRepository.GetById(id);
            if (technicalResponsibility is null)
                throw new ExceptionBadRequest("Responsabilidade técnica com o id " + id + " informado não foi encontrado.");

            return _mapper.Map<TechnicalResponsibilityDTO>(technicalResponsibility);
        }

        public override async Task<TechnicalResponsibilityDTO> Create(TechnicalResponsibilityDTO technicalResponsibilityDto)
        {
            var errors = new List<FieldError>();
            if (technicalResponsibilityDto is null)
                throw new ExceptionBadRequest("A Responsabilidade técnica não pode ser nula.");

            return _mapper.Map<TechnicalResponsibilityDTO>(await base.Create(technicalResponsibilityDto));
        }
        public override async Task Update(TechnicalResponsibilityDTO technicalResponsibilityDto, int id)
        {
            var errors = new List<FieldError>();
            if (technicalResponsibilityDto is null)
                throw new ExceptionBadRequest("A Responsabilidade técnica não pode ser nula.");

            if (errors.Count() > 0)
                throw new ExceptionBadRequest("Erros na requisição", errors);

            await base.Update(technicalResponsibilityDto, id);
        }
        public override async Task Remove(int id)
        {

            var technicalResponsibility = await _technicalResponsibilityRepository.GetById(id);
            if (technicalResponsibility is null)
                throw new ExceptionConflict("Responsabilidade técnica com o id " + id + " informado não foi encontrada.");

            await base.Remove(id);
        }

    }
}
