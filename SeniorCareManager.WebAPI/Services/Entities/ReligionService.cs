using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ReligionService : GenericService<Religion, ReligionDTO>, IReligionService
{
    private readonly IReligionRepository _religionRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public ReligionService(IReligionRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
    {
        _religionRepository = repository;
        _mapper = mapper;
        _context = context;
    }
    public override async Task<ReligionDTO> GetById(int id)
    {
        /*
         * Busca por id o registro
         * Caso não for encontrado retorna notFound 
         */
        var religion = await _religionRepository.GetById(id);
        if (religion is null)
            throw new ExceptionNotFound("Religião com o id " + id + " informado não foi encontrada.");

        return _mapper.Map<ReligionDTO>(religion);
    }
    public override async Task<ReligionDTO> Create(ReligionDTO religionDto)
    {
        /*
        * Verifica se tem nomes duplicados
        * Verifica se não é nulo
        * Caso der erros retorna lista de erros
        */

        var errors = new List<FieldError>();
        if (religionDto is null)
            throw new ExceptionBadRequest("A Religião não pode ser nula.");

        if (await CheckDuplicates(religionDto.Name))
            throw new ExceptionConflict("Nome já existente.");

        return _mapper.Map<ReligionDTO>( await base.Create(religionDto) );
    }
    public override async Task Update(ReligionDTO religionDto, int id)
    {
        /*
         * Atualiza um cargo
         * Verifica se tem nomes duplicados 
         * Verifica se o id inserido está correto
         * Caso der erros retorna lista de erros
         */
        var errors = new List<FieldError>();
        if (religionDto is null)
            throw new ExceptionBadRequest("A Religião não pode ser nula.");

        if (religionDto.Id != id)
            throw new ExceptionBadRequest("O id da religião dever ser o mesmo.");

        if (await CheckDuplicates(religionDto.Name))
            throw new ExceptionConflict("Nome já existente.");

        if (errors.Count() > 0)
            throw new ExceptionBadRequest("Erros na requisição", errors);

        await base.Update(religionDto, id);
    }
    public override async Task Remove(int id)
    {
        /*
         * Remove o cargo
         * Se estiver sendo utilizado não apaga(na Resident)
         */
        var religion = await _religionRepository.GetById(id);
        var isReligionnInUse = await _context.Set<Resident>().AnyAsync(ra => ra.ReligionId == id);
        if (isReligionnInUse)
        {
            throw new InvalidOperationException("Essea religião não pode ser removida pois está vinculada a um ou mais registros.");
        }
        if (religion is null)
            throw new ExceptionNotFound("Religião com o id " + id + " informado não foi encontrada.");
        await base.Remove(id);
    }
    public async Task<bool> CheckDuplicates(string nome)
    {
        /*
         * Verifica se tem algum nome igual de religião
         */
        var religions = await _religionRepository.Get();
        return religions.Any(r => StringUtils.CompareString(r.Name, nome));
    }
}
