using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class PositionService : GenericService<Position, PositionDTO>, IPositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionService(IPositionRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _positionRepository = repository;
            _mapper = mapper;
        }
        public override async Task<PositionDTO> GetById(int id)
        {
            var errors = new List<FieldError>();
            var position = await _positionRepository.GetById(id);
            if (position is null)
                throw new ExceptionBadRequest("Cargo com o id " + id + " informado não foi encontrado.");

            return _mapper.Map<PositionDTO>(position);
        }

        public override async Task Create(PositionDTO positionDto)
        {
            var errors = new List<FieldError>();

            if (positionDto is null)
                throw new ExceptionBadRequest("O Cargo não pode ser nulo.");



            if (await CheckDuplicates(positionDto.Name))
                throw new ExceptionConflict("Nome duplicado.");

            await base.Create(positionDto);
        }
        public override async Task Update(PositionDTO positionDto, int id)
        {
            var errors = new List<FieldError>();
            if (positionDto is null)
                throw new ExceptionBadRequest("O Cargo não pode ser nulo.");

            if (positionDto.Id != id)
                throw new ExceptionBadRequest("O id de Cargo dever ser o mesmo.");

            if (await CheckDuplicates(positionDto.Name))
                errors.Add(new FieldError{Field = "Nome", Message = "Nome duplicado."});

            if (errors.Count() > 0)
                throw new ExceptionBadRequest("Erros na requisição", errors);

            await base.Update(positionDto, id);
        }
        public override async Task Remove(int id)
        {

            var position = await _positionRepository.GetById(id);
            if (position is null)
                throw new ExceptionConflict("Cargo com o id " + id + " informado não foi encontrado.");

            await base.Remove(id);
        }
        public async Task<bool> CheckDuplicates(string name)
        {
            var positions = await _positionRepository.Get();
            return positions.Any(r =>
                StringUtils.CompareString(r.Name, name)
            );
        }
    }
}
