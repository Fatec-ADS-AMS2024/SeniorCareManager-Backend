using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
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
        private readonly AppDbContext _context;

        public PositionService(IPositionRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _positionRepository = repository;
            _mapper = mapper;
            _context = context;
        }
        public override async Task<PositionDTO> GetById(int id)
        {
            /*
             * Busca por id o registro
             * Caso não for encontrado retorna nNotFound 
             */
            var errors = new List<FieldError>();
            var position = await _positionRepository.GetById(id);
            if (position is null)
                throw new ExceptionNotFound("Cargo com o id " + id + " informado não foi encontrado.");

            return _mapper.Map<PositionDTO>(position);
        }

        public override async Task<PositionDTO> Create(PositionDTO positionDto)
        {
            /*
             * Verifica se tem nomes duplicados
             * Verifica se não é nulo
             * Caso der erros retorna lista de erros
             */
            var errors = new List<FieldError>();

            if (positionDto is null)
                throw new ExceptionBadRequest("O Cargo não pode ser nulo.");

            if (await CheckDuplicates(positionDto.Name))
                throw new ExceptionConflict("Nome duplicado.");

            return _mapper.Map<PositionDTO>( await base.Create(positionDto) );
        }
        public override async Task Update(PositionDTO positionDto, int id)
        {
            /*
             * Atualiza um cargo
             * Verifica se tem nomes duplicados 
             * Verifica se o id inserido está correto
             * Caso der erros retorna lista de erros
             */
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
            /*
             * Remove o cargo
             * Se estiver sendo utilizado não apaga(na Employee e TechnicalResponsibility)
             */
            var position = await _positionRepository.GetById(id);
            var isPositionInUse = await _context.Set<Employee>().AnyAsync(ra => ra.PositionId == id)/* || await _context.Set<TechnicalResponsibility>().AnyAsync(ra => ra.PositionId == id)*/;//Mudar na task TechnicalResponsibility 
            if (isPositionInUse)
            {
                throw new InvalidOperationException("Esse cargo não pode ser removido pois está vinculada a um ou mais registros.");
            }
            if (position is null)
                throw new ExceptionNotFound("Cargo com o id " + id + " informado não foi encontrado.");

            await base.Remove(id);
        }
        public async Task<bool> CheckDuplicates(string name)
        {
            /*
             * Verifica se tem algum nome igual de cargo
             */
            var positions = await _positionRepository.Get();
            return positions.Any(r =>
                StringUtils.CompareString(r.Name, name)
            );
        }
    }
}
