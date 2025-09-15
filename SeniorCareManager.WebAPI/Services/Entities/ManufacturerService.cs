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

        if (await CheckDuplicates(manufacturerDto.CpfCnpj))
            throw new ExceptionConflict("CPF/CNPJ duplicado.");

        await base.Create(manufacturerDto);
    }

    public override async Task Update(ManufacturerDTO manufacturerDto, int id)
    {
        var errors = new List<FieldError>();

        if (manufacturerDto is null)
            throw new ExceptionBadRequest("O Fabricante não pode ser nulo.");

        if (await CheckDuplicates(manufacturerDto.CpfCnpj))
            errors.Add(new FieldError { Field = "CpfCnpj", Message = "CPF/CNPJ duplicado." });

        if (errors.Count > 0)
            throw new ExceptionBadRequest("Erros na requisição", errors);

        await base.Update(manufacturerDto, id);
    }

    public override async Task Remove(int id)
    {
        var manufacturer = await _manufacturerRepository.GetById(id);
        if (manufacturer is null)
            throw new ExceptionConflict("Fabricante com o id " + id + " informado não foi encontrado.");

        await base.Remove(id);
    }

    public async Task<bool> CheckDuplicates(string cpfCnpj)
    {
        var manufacturers = await _manufacturerRepository.Get();
        return manufacturers.Any(r =>
            StringUtils.CompareString(r.CpfCnpj, cpfCnpj)
        );
    }
}