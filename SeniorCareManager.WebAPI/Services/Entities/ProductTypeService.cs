using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ProductTypeService : GenericService<ProductType, ProductTypeDTO>, IProductTypeService
{
    private readonly IProductTypeRepository _repository;
    private readonly IMapper _mapper;

    public ProductTypeService(IProductTypeRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override async Task<ProductTypeDTO> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity == null)
            throw new ExceptionNotFound($"Tipo de produto com id {id} n�o encontrado.");

        return _mapper.Map<ProductTypeDTO>(entity);
    }

    public override async Task<ProductTypeDTO> Create(ProductTypeDTO dto)
    {
        Execute.Executar(dto);

        if (await CheckDuplicateName(dto.Name))
            throw new ExceptionConflict("J� existe um tipo de produto com este nome.");

        return await base.Create(dto);
    }

    public override async Task Update(ProductTypeDTO dto, int id)
    {
        Execute.Executar(dto);

        if (await CheckDuplicateName(dto.Name, id))
            throw new ExceptionConflict("J� existe um tipo de produto com este nome.");

        await base.Update(dto, id);
    }

    public override async Task Remove(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity == null)
            throw new ExceptionNotFound($"Tipo de produto com id {id} n�o encontrado.");

        await base.Remove(id);
    }

    private async Task<bool> CheckDuplicateName(string name, int currentId = 0)
    {
        var allTypes = await _repository.Get();
        return allTypes.Any(pt =>
            pt.Id != currentId &&
            StringUtils.CompareString(pt.Name.Trim(), name.Trim())
        );
    }

    public async Task<bool> IsDuplicateNameAsync(string name, int id = 0)
    {
        return await CheckDuplicateName(name, id);
    }

    public async Task<int?> GetGroupIdIfDuplicateAsync(string name, int currentId = 0)
    {
        var allTypes = await _repository.Get();
        var match = allTypes
            .Where(pt => pt.Id != currentId && StringUtils.CompareString(pt.Name.Trim(), name.Trim()))
            .FirstOrDefault();

        return match?.ProductGroupId;
    }
}
