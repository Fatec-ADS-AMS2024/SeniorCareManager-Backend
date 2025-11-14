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

namespace SeniorCareManager.WebAPI.Services.Entities;

public class HealthInsurancePlanService : GenericService<HealthInsurancePlan, HealthInsurancePlanDTO>, IHealthInsurancePlanService
{
    private readonly IHealthInsurancePlanRepository _healthInsurancePlanRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    public HealthInsurancePlanService(IHealthInsurancePlanRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
    {
        _healthInsurancePlanRepository = repository;
        _mapper = mapper;
        _context = context;
    }
    /*
     * Busca por id o registro
     * Caso não for encontrado retorna notFound
     */
    public override async Task<HealthInsurancePlanDTO> GetById(int id)
    {
        var errors = new List<FieldError>();
        var healthInsurancePlan = await _healthInsurancePlanRepository.GetById(id);
        if (healthInsurancePlan is null)
            throw new ExceptionNotFound("Plano de saúde com o id " + id + " informado não foi encontrada.");

        return _mapper.Map<HealthInsurancePlanDTO>(healthInsurancePlan);
    }
    /*
    * Verifica se tem nomes e abreviações duplicadas
    * Verifica se não é nulo
    * Caso der erros retorna lista de erros
    */
    public override async Task<HealthInsurancePlanDTO> Create(HealthInsurancePlanDTO healthInsurancePlanDto)
    {
        var errors = new List<FieldError>();
        if (healthInsurancePlanDto is null)
            throw new ExceptionBadRequest("O Plano de Saúde não pode ser nulo.");

        if (await CheckDuplicates(p => p.Name, healthInsurancePlanDto.Name, healthInsurancePlanDto.Id))
            throw new ExceptionConflict("Nome duplicado.");

        if (await CheckDuplicates(p => p.Abbreviation, healthInsurancePlanDto.Abbreviation, healthInsurancePlanDto.Id))
            throw new ExceptionConflict("Abreviação duplicada.");

        return _mapper.Map<HealthInsurancePlanDTO>( await base.Create(healthInsurancePlanDto) );
    }
    /*
     * Atualiza um plano de saúde
     * Verifica se tem nomes e abreviações duplicadas
     * Verifica se o id inserido está correto
     * Caso der erros retorna lista de erros
     */
    public override async Task Update(HealthInsurancePlanDTO healthInsurancePlanDto, int id)
    {
        var errors = new List<FieldError>();
        if (healthInsurancePlanDto is null)
            throw new ExceptionBadRequest("O Plano de Saúde não pode ser nulo.");

        if (healthInsurancePlanDto.Id != id)
            throw new ExceptionBadRequest("O id de Plano de Saúde dever ser o mesmo.");

        if (await CheckDuplicates(p => p.Name, healthInsurancePlanDto.Name, healthInsurancePlanDto.Id))
            errors.Add(new FieldError { Field = "Nome", Message = "Nome duplicado." });

        if (await CheckDuplicates(p => p.Abbreviation, healthInsurancePlanDto.Abbreviation, healthInsurancePlanDto.Id))
            errors.Add(new FieldError { Field = "Abreviação", Message = "Abreviação duplicada." });

        if (errors.Count() > 0)
            throw new ExceptionBadRequest("Erros na requisição", errors);

        await base.Update(healthInsurancePlanDto, id);
    }
    /*
     * Remove o cargo
     * Se estiver sendo utilizado não apaga(na Resident)
     */
    public override async Task Remove(int id)
    {
        var errors = new List<FieldError>();
        var healthInsurancePlan = await _healthInsurancePlanRepository.GetById(id);
        var isReligionnInUse = await _context.Set<Resident>().AnyAsync(ra => ra.ReligionId == id);
        if (isReligionnInUse)
        {
            throw new ExceptionConflict("Esse plano de saúde não pode ser removido pois está vinculada a um ou mais registros.");
        }
        if (healthInsurancePlan is null)
            throw new ExceptionNotFound("Plano de saúde com o id " + id + " informado não foi encontrada.");

        await base.Remove(id);
    }
    /*
     * Verifica se um registro já está cadastrado
     */
    public async Task<bool> CheckDuplicates(Func<HealthInsurancePlan, string?> selector, string? valor, int idIgnor)
    {
        var planos = await _healthInsurancePlanRepository.Get();
        return planos.Any(p =>
            p.Id != idIgnor &&
            StringUtils.CompareString(selector(p)!, valor)
        );
    }

}
