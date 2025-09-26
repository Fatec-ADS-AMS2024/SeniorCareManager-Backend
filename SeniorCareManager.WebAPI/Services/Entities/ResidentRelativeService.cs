using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;


namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class ResidentRelativeService : GenericService<ResidentRelative, ResidentRelativeDTO>   
    {
        private readonly IResidentRelativeRepository _residentRelativeRepository;
        private readonly IMapper _mapper;
        public ResidentRelativeService(IResidentRelativeRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _residentRelativeRepository = repository;
            _mapper = mapper;
        }
        public override async Task<ResidentRelativeDTO> GetById(int id)
        {
            var errors = new List<FieldError>();
            var residentRelative = await _residentRelativeRepository.GetById(id);
            if (residentRelative is null)
                throw new ExceptionBadRequest("Parente do residente com o id " + id + " informado não foi encontrado.");
            return _mapper.Map<ResidentRelativeDTO>(residentRelative);
        }
        public override async Task Create(ResidentRelativeDTO residentRelativeDto)
        {
            var errors = new List<FieldError>();
            if (residentRelativeDto is null)
                throw new ExceptionBadRequest("O Parente do Residente não pode ser nulo.");
            if (errors.Count() > 0)
                throw new ExceptionBadRequest("Erros na requisição", errors);
            await base.Create(residentRelativeDto);
        }
        public override async Task Update(ResidentRelativeDTO residentRelativeDto, int id)
        {
            var errors = new List<FieldError>();
            if (residentRelativeDto is null)
                throw new ExceptionBadRequest("O Parente do Residente não pode ser nulo.");
            if (errors.Count() > 0)
                throw new ExceptionBadRequest("Erros na requisição", errors);
            await base.Update(residentRelativeDto, id);
        }

        public override async Task Remove(int id)
        {
            var errors = new List<FieldError>();
            var residentRelative = await _residentRelativeRepository.GetById(id);
            if (residentRelative is null)
                throw new ExceptionBadRequest("Parente do residente com o id " + id + " informado não foi encontrado.");
            await base.Remove(id);
        }
    }
}
