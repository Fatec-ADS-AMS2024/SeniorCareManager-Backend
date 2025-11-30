using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;
public class ProductGroupService : GenericService<ProductGroup, ProductGroupDTO>, IProductGroupService
{
    private readonly IProductGroupRepository _repository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;
    private const int NAME_MAX_LENGTH = 50;

    public ProductGroupService(IProductGroupRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _context = context;
    }

    public override async Task<ProductGroupDTO> GetById(int id)
    {

        /*
         * Busca por id o registro de grupo de produto.
         * Se não encontrado lança ExceptionBadRequest informando que o grupo não existe.
         */

        var entity = await _repository.GetById(id);
        if (entity is null)
            throw new ExceptionNotFound($"Grupo de produto com id {id} não encontrado.");

        return _mapper.Map<ProductGroupDTO>(entity);
    }

    public override async Task<ProductGroupDTO> Create(ProductGroupDTO dto)
    {

        /*
         * Cria um novo grupo de produto.
         * Valida:
         *  - dto não nulo
         *  - tamanho máximo do campo Name (conforme constante NAME_MAX_LENGTH)
         *  - nome duplicado (verifica com IsDuplicateNameAsync)
         * Acumula erros em uma lista de FieldError e lança ExceptionBadRequest se houver.
         * Em caso de nome duplicado lança ExceptionConflict.
         */

        var errors = new List<FieldError>();

        if (dto is null)
            throw new ExceptionBadRequest("O Grupo de produto não pode ser nulo.");

        // Validação de tamanho
        if (!string.IsNullOrEmpty(dto.Name) && dto.Name.Length > NAME_MAX_LENGTH)
            errors.Add(new FieldError { Field = "Name", Message = $"O campo 'Nome' deve ter no máximo {NAME_MAX_LENGTH} caracteres." });

        // Verifica duplicidade de nome (seguindo padrão HealthInsurancePlan)
        if (await CheckDuplicates(p => p.Name, dto.Name, dto.Id))
            throw new ExceptionConflict("Nome duplicado.");

        if (errors.Count > 0)
            throw new ExceptionBadRequest("Erros na requisição", errors);

        return _mapper.Map<ProductGroupDTO>(await base.Create(dto));
    }

    public override async Task Update(ProductGroupDTO dto, int id)
    {

        /*
         * Atualiza um grupo de produto existente.
         * Valida:
         *  - dto não nulo
         *  - dto.Id corresponde ao id informado na rota
         *  - execução de formatações/validações via Execute.Executar(dto)
         *  - tamanho máximo do campo Name
         *  - nome duplicado (excluindo o próprio registro)
         * Acumula erros em FieldError e lança ExceptionBadRequest caso existam.
         */

        var errors = new List<FieldError>();

        if (dto is null)
            throw new ExceptionBadRequest("O Grupo de produto não pode ser nulo.");

        if (dto.Id != id)
            throw new ExceptionBadRequest("O id do Grupo de produto deve ser o mesmo.");

        Execute.Executar(dto);

        // Validação de tamanho
        if (!string.IsNullOrEmpty(dto.Name) && dto.Name.Length > NAME_MAX_LENGTH)
            errors.Add(new FieldError { Field = "Name", Message = $"O campo 'Nome' deve ter no máximo {NAME_MAX_LENGTH} caracteres." });

        if (await CheckDuplicates(p => p.Name, dto.Name, dto.Id))
            errors.Add(new FieldError { Field = "Name", Message = "Nome duplicado." });

        if (errors.Count > 0)
            throw new ExceptionBadRequest("Erros na requisição", errors);

        await base.Update(dto, id);
    }

    public override async Task Remove(int id)
    {

        /*
         * Remove um grupo de produto por id.
         * Verifica se o registro existe; se não existir lança ExceptionConflict.
         * Observação: se futuramente for necessário verificar relacionamentos (ex.: ProductType),
         * essa checagem deverá ser adicionada aqui antes de remover.
         */

        var group = await _repository.GetById(id);
        if (group is null)
            throw new ExceptionNotFound($"Grupo de produto com id {id} não encontrado.");

        // Validação de vínculo: impede exclusão se existir ProductType vinculado
        var isInUse = await _context.Set<ProductType>().AnyAsync(pt => pt.ProductGroupId == id);
        if (isInUse)
            throw new ExceptionConflict("Esse grupo de produto não pode ser removido pois está vinculado a um ou mais tipos de produto.");

        await base.Remove(id);
    }

    public async Task<bool> CheckDuplicates(Func<ProductGroup, string?> selector, string? valor, int idIgnor)
    {
        var groups = await _repository.Get();
        return groups.Any(p =>
            p.Id != idIgnor &&
            StringUtils.CompareString(selector(p)!, valor)
        );
    }

    public async Task<bool> IsDuplicateNameAsync(string name, int id = 0)
    {
        return await CheckDuplicates(p => p.Name, name, id);
    }
}
