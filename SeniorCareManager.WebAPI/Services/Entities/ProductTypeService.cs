using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SeniorCareManager.WebAPI.Data;
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
    private readonly IProductGroupRepository _productGroupRepository;
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public ProductTypeService(
        IProductTypeRepository repository,
        IProductGroupRepository productGroupRepository,
        IMapper mapper,
        AppDbContext context
    ) : base(repository, mapper)
    {
        _repository = repository;
        _productGroupRepository = productGroupRepository;
        _mapper = mapper;
        _context = context;
    }

    public override async Task<ProductTypeDTO> GetById(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity == null)
            throw new ExceptionNotFound($"Tipo de produto com id {id} não encontrado.");

        return _mapper.Map<ProductTypeDTO>(entity);
    }

    public override async Task<ProductTypeDTO> Create(ProductTypeDTO dto)
    {
        Execute.Executar(dto);

        // Verifica se o grupo existe
        var group = await _productGroupRepository.GetById(dto.ProductGroupId);
        if (group == null)
            throw new ExceptionBadRequest("Grupo de produto informado não existe.");

        if (await CheckDuplicateName(dto.Name))
            throw new ExceptionConflict("Já existe um tipo de produto com este nome.");

        return await base.Create(dto);
    }

    public override async Task Update(ProductTypeDTO dto, int id)
    {
        if (dto is null)
            throw new ExceptionBadRequest("O Tipo de produto não pode ser nulo.");

        if (dto.Id != id)
            throw new ExceptionBadRequest("O id do Tipo de produto deve ser o mesmo.");

        Execute.Executar(dto);

        // Verifica se o registro existe
        var existing = await _repository.GetById(id);
        if (existing is null)
            throw new ExceptionNotFound($"Tipo de produto com id {id} não encontrado.");

        // Verifica se o grupo existe
        var group = await _productGroupRepository.GetById(dto.ProductGroupId);
        if (group == null)
            throw new ExceptionBadRequest("Grupo de produto informado não existe.");

        if (await CheckDuplicateName(dto.Name, id))
            throw new ExceptionConflict("Já existe um tipo de produto com este nome.");

        await base.Update(dto, id);
    }

    public override async Task Remove(int id)
    {
        var entity = await _repository.GetById(id);
        if (entity == null)
            throw new ExceptionNotFound($"Tipo de produto com id {id} não encontrado.");

        // Impede remoção se houver produtos vinculados
        var isInUse = await _context.Set<Product>().AnyAsync(p => p.ProductTypeId == id);
        if (isInUse)
            throw new ExceptionConflict("Esse tipo de produto não pode ser removido pois está vinculado a um ou mais produtos.");

        await base.Remove(id);
    }

    private async Task<bool> CheckDuplicateName(string name, int currentId = 0)
    {
        var allTypes = await _repository.Get();
        return allTypes.Any(pt =>
            pt.Id != currentId &&
            StringUtils.CompareString(pt.Name?.Trim() ?? string.Empty, name?.Trim() ?? string.Empty)
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
            .Where(pt => pt.Id != currentId && StringUtils.CompareString(pt.Name?.Trim() ?? string.Empty, name?.Trim() ?? string.Empty))
            .FirstOrDefault();

        return match?.ProductGroupId;
    }
}
