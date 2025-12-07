using AutoMapper;
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
    public class UnitOfMeasureService : GenericService<UnitOfMeasure, UnitOfMeasureDTO>, IUnitOfMeasureService
    {
        private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public UnitOfMeasureService(IUnitOfMeasureRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _unitOfMeasureRepository = repository;
            _mapper = mapper;
            _context = context;
        }
        public override async Task<UnitOfMeasureDTO> GetById(int id)
        {
            /*
             * Busca por id o registro
             * Caso não for encontrado retorna notFound 
             */
            var unitOfMeasure = await _unitOfMeasureRepository.GetById(id);
            if (unitOfMeasure is null)
                throw new ExceptionNotFound("Unidade de medida com o id " + id + " informado não foi encontrada.");

            return _mapper.Map<UnitOfMeasureDTO>(unitOfMeasure);
        }
        public override async Task<UnitOfMeasureDTO> Create(UnitOfMeasureDTO unitOfMeasureDTO)
        {
            /*
            * Verifica se tem nomes duplicados
            * Verifica se não é nulo
            * Caso der erros retorna lista de erros
            */

            var errors = new List<FieldError>();
            if (unitOfMeasureDTO is null)
                throw new ExceptionBadRequest("A Unidade de medida não pode ser nula.");

            if (await CheckDuplicates(unitOfMeasureDTO.Abbreviation))
                throw new ExceptionConflict("Abreviação já existente.");

            return _mapper.Map<UnitOfMeasureDTO>(await base.Create(unitOfMeasureDTO));
        }
        public override async Task Update(UnitOfMeasureDTO unitOfMeasureDTO, int id)
        {
            /*
             * Atualiza um cargo
             * Verifica se tem nomes duplicados 
             * Verifica se o id inserido está correto
             * Caso der erros retorna lista de erros
             */
            var errors = new List<FieldError>();
            if (unitOfMeasureDTO is null)
                throw new ExceptionBadRequest("A Unidade de medida não pode ser nula.");

            if (unitOfMeasureDTO.Id != id)
                throw new ExceptionNotFound("O id da unidade de medida dever ser o mesmo.");

            if (await CheckDuplicates(unitOfMeasureDTO.Abbreviation))
                throw new ExceptionConflict("Abreviação já existente.");

            if (errors.Count() > 0)
                throw new ExceptionBadRequest("Erros na requisição", errors);

            await base.Update(unitOfMeasureDTO, id);
        }

        public override async Task Remove(int id)
        {
            /*
             * Remove o cargo
             */
            var religion = await _unitOfMeasureRepository.GetById(id);
            
            if (religion is null)
                throw new ExceptionNotFound("Unidade de medida com o id " + id + " informado não foi encontrada.");
            await base.Remove(id);
        }
        public async Task<bool> CheckDuplicates(string abbreviation)
        {
            /*
             * Verifica se tem algum nome igual de abreviação
             */
            var unitOfMeasures = await _unitOfMeasureRepository.Get();
            return unitOfMeasures.Any(r => StringUtils.CompareString(r.Abbreviation, abbreviation));

        }
        }
}
