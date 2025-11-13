using AutoMapper;
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
    private const int NAME_MAX_LENGTH = 50;

    public ProductGroupService(IProductGroupRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override async Task<ProductGroupDTO> GetById(int id)
    {

        /*
         * Busca por id o registro de grupo de produto.
         * Se não encontrado lança ExceptionBadRequest informando que o grupo não existe.
         */

        var entity = await _repository.GetById(id);
        if (entity is null)
            throw new ExceptionBadRequest($"Grupo de produto com id {id} não encontrado.");

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

        if (await IsDuplicateNameAsync(dto.Name))
            throw new ExceptionConflict("Já existe um grupo de produto com este nome.");

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

        if (await IsDuplicateNameAsync(dto.Name, id))
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

        var entity = await _repository.GetById(id);
        if (entity is null)
            throw new ExceptionConflict($"Grupo de produto com id {id} não encontrado.");

        await base.Remove(id);
    }

    public async Task<bool> IsDuplicateNameAsync(string name, int id = 0)
    {

        /*
         * Verifica se já existe outro grupo com o mesmo nome.
         * Exclui o registro com o id fornecido (quando id != 0) da verificação.
         * Usa StringUtils.CompareString para comparação normalizada.
         */

        var allGroups = await _repository.Get();
        return allGroups.Any(g =>
            g.Id != id &&
            StringUtils.CompareString(g.Name.Trim(), name.Trim())
        );
    }
}