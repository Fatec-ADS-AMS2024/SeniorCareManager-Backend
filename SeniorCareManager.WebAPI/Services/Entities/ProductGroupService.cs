using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
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

    public ProductGroupService(IProductGroupRepository repository, IMapper mapper)
        : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override async Task<ProductGroupDTO> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity is null)
            throw new ExceptionNotFound($"Grupo de produto com id {id} não encontrado.");

        return _mapper.Map<ProductGroupDTO>(entity);
    }

    public override async Task Create(ProductGroupDTO dto)
    {
        Execute.Executar(dto);

        if (await IsDuplicateNameAsync(dto.Name))
            throw new ExceptionConflict("Já existe um grupo de produto com este nome.");

        await base.Create(dto);
    }

    public override async Task Update(ProductGroupDTO dto, int id)
    {
        Execute.Executar(dto);

        if (await IsDuplicateNameAsync(dto.Name, id))
            throw new ExceptionConflict("Já existe um grupo de produto com este nome.");

        await base.Update(dto, id);
    }

    public override async Task Remove(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity is null)
            throw new ExceptionNotFound($"Grupo de produto com id {id} não encontrado.");

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
