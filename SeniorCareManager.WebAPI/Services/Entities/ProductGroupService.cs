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

    public ProductGroupService(IProductGroupRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override async Task<ProductGroupDTO> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity is null)
            throw new ExceptionBadRequest($"Grupo de produto com id {id} n�o encontrado.");

        return _mapper.Map<ProductGroupDTO>(entity);
    }

    public override async Task<ProductGroupDTO> Create(ProductGroupDTO dto)
    {
        var errors = new List<FieldError>();

        if (dto is null)
            throw new ExceptionBadRequest("O Grupo de produto não pode ser nulo.");

        if (await IsDuplicateNameAsync(dto.Name))
            throw new ExceptionConflict("J� existe um grupo de produto com este nome.");

        return _mapper.Map<ProductGroupDTO>(await base.Create(dto));
    }

    public override async Task Update(ProductGroupDTO dto, int id)
    {
        var errors = new List<FieldError>();

        if (dto is null)
            throw new ExceptionBadRequest("O Grupo de produto n�o pode ser nulo.");

        if (dto.Id != id)
            throw new ExceptionBadRequest("O id do Grupo de produto deve ser o mesmo.");

        Execute.Executar(dto);

        if (await IsDuplicateNameAsync(dto.Name, id))
            errors.Add(new FieldError { Field = "Name", Message = "Nome duplicado." });

        if (errors.Count > 0)
            throw new ExceptionBadRequest("Erros na requisi��o", errors);

        await base.Update(dto, id);
    }

    public override async Task Remove(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity is null)
            throw new ExceptionConflict($"Grupo de produto com id {id} n�o encontrado.");

        await base.Remove(id);
    }

    public async Task<bool> IsDuplicateNameAsync(string name, int id = 0)
    {
        var allGroups = await _repository.Get();
        return allGroups.Any(g =>
            g.Id != id &&
            StringUtils.CompareString(g.Name.Trim(), name.Trim())
        );
    }
}