using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ManufacturerService : GenericService<Manufacturer, ManufacturerDTO>, IManufacturerService
{
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IMapper _mapper;

    public ManufacturerService(IManufacturerRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _manufacturerRepository = repository;
        _mapper = mapper;
    }

    public override async Task<ManufacturerDTO> GetById(int id)
    {
        var errors = new List<FieldError>();
        var manufacturer = await _manufacturerRepository.GetById(id);
        if (manufacturer is null)
            throw new ExceptionBadRequest("Fabricante com o id " + id + " informado não foi encontrado.");

        return _mapper.Map<ManufacturerDTO>(manufacturer);
    }

    public override async Task Create(ManufacturerDTO manufacturerDto)
    {
        var errors = new List<FieldError>();
        if (manufacturerDto is null)
            throw new ExceptionBadRequest("O Fabricante não pode ser nulo.");

        if (await CheckDuplicates(m => m.CpfCnpj, manufacturerDto.CpfCnpj, manufacturerDto.Id))
            throw new ExceptionConflict("CPF/CNPJ duplicado.");

        if (await CheckDuplicates(m => m.TradeName, manufacturerDto.TradeName, manufacturerDto.Id))
            throw new ExceptionConflict("Nome comercial duplicado.");

        if (await CheckDuplicates(m => m.Phone, manufacturerDto.Phone, manufacturerDto.Id))
            throw new ExceptionConflict("Telefone duplicado.");

        await base.Create(manufacturerDto);
    }

    public override async Task Update(ManufacturerDTO manufacturerDto, int id)
    {
        var errors = new List<FieldError>();
        if (manufacturerDto is null)
            throw new ExceptionBadRequest("O Fabricante não pode ser nulo.");

        if (manufacturerDto.Id != id)
            throw new ExceptionBadRequest("O id de Fabricante deve ser o mesmo.");

        if (await CheckDuplicates(m => m.CpfCnpj, manufacturerDto.CpfCnpj, manufacturerDto.Id))
            errors.Add(new FieldError { Field = "CpfCnpj", Message = "CPF/CNPJ duplicado." });

        if (await CheckDuplicates(m => m.TradeName, manufacturerDto.TradeName, manufacturerDto.Id))
            errors.Add(new FieldError { Field = "TradeName", Message = "Nome comercial duplicado." });

        if (await CheckDuplicates(m => m.Phone, manufacturerDto.Phone, manufacturerDto.Id))
            errors.Add(new FieldError { Field = "Phone", Message = "Telefone duplicado." });

        if (errors.Count > 0)
            throw new ExceptionBadRequest("Erros na requisição", errors);

        await base.Update(manufacturerDto, id);
    }

    public override async Task Remove(int id)
    {
        var errors = new List<FieldError>();
        var manufacturer = await _manufacturerRepository.GetById(id);
        if (manufacturer is null)
            throw new ExceptionBadRequest("Fabricante com o id " + id + " informado não foi encontrado.");

        await base.Remove(id);
    }

    public async Task<bool> CheckDuplicates(Func<Manufacturer, string?> selector, string? valor, int idIgnor)
    {
        var fabricantes = await _manufacturerRepository.Get();
        return fabricantes.Any(m =>
            m.Id != idIgnor &&
            StringUtils.CompareString(selector(m)!, valor)
        );
    }
}