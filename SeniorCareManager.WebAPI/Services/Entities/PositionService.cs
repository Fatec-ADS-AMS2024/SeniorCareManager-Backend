

using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
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
            var position = await _positionRepository.GetById(id);
            if (position is null)
                throw new ArgumentNullException("Cargo com o id " + id + " informado não foi encontrado.");

            return _mapper.Map<PositionDTO>(position);
        }
        public override async Task Create(PositionDTO positionDto)
        {
            if (positionDto is null)
                throw new ArgumentNullException("O Cargo não pode ser nulo.");

            if (PositionDTO.IsFilledString(positionDto.Name))
                throw new ArgumentException("Nome Inválido.");

            if (await CheckDuplicates(positionDto.Name))
                throw new InvalidOperationException("Nome duplicadoa.");

            await base.Create(positionDto);
        }
        public override async Task Update(PositionDTO positionDto, int id)
        {
            if (positionDto is null)
                throw new ArgumentNullException("O Cargo não pode ser nulo.");

            if (PositionDTO.IsFilledString(positionDto.Name))
                throw new ArgumentException("Nome Inválido.");

            if (await CheckDuplicates(positionDto.Name))
                throw new InvalidOperationException("Nome duplicado.");

            await base.Update(positionDto, id);
        }
        public override async Task Remove(int id)
        {
            var position = await _positionRepository.GetById(id);
            if (position is null)
                throw new ArgumentNullException("Cargo com o id " + id + " informado não foi encontrado.");

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
