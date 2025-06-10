using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Contracts;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Entities;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

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
        var manufacturer = await _manufacturerRepository.GetById(id);
        if (manufacturer is null)
            throw new KeyNotFoundException("Fabricante com o id " + id + " informado não foi encontrado.");

        return _mapper.Map<ManufacturerDTO>(manufacturer);
    }

    public override async Task Create(ManufacturerDTO manufacturerDto)
    {
        if (manufacturerDto is null)
            throw new ArgumentNullException(nameof(manufacturerDto), "Fabricante não pode ser nulo.");

        if (!manufacturerDto.CheckName())
            throw new ArgumentException("Nome Inválido.");

        if (await CheckDuplicates(manufacturerDto))
            throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

        if (!manufacturerDto.CheckEmail())
            throw new ArgumentException("Email inválido.");

        if (!manufacturerDto.CheckCpfCnpj())
            throw new ArgumentException("CPF/CNPJ inválido.");

        if(!manufacturerDto.CheckPhone())
            throw new ArgumentException("Telefone inválido.");

        if (await _manufacturerRepository.GetById(manufacturerDto.Id) is not null)
            return; // Se já existe um fabricante com esse ID, apenas continua

        await base.Create(manufacturerDto);
    }

    public override async Task Update(ManufacturerDTO manufacturerDto, int id)
    {
        if (manufacturerDto is null)
            throw new ArgumentNullException(nameof(manufacturerDto), "Fabricante não pode ser nulo.");

        if (!manufacturerDto.CheckName())
            throw new ArgumentException("Nome Inválido.");

        if (!manufacturerDto.CheckEmail())
            throw new ArgumentException("Email inválido.");

        if (!manufacturerDto.CheckCpfCnpj())
            throw new ArgumentException("CPF/CNPJ inválido.");

        if (!manufacturerDto.CheckPhone())
            throw new ArgumentException("Telefone inválido.");

        if (await CheckDuplicates(manufacturerDto))
            throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

        await base.Update(manufacturerDto, id);
    }

    public override async Task Remove(int id)
    {
        var manufacturer = await _manufacturerRepository.GetById(id);
        if (manufacturer is null)
            throw new KeyNotFoundException("Fabricante com o id " + id + " informado não foi encontrado.");

        await base.Remove(id);
    }

    public async Task<bool> CheckDuplicates(ManufacturerDTO dto)
    {
        var manufacturers = await _manufacturerRepository.Get();
        return manufacturers.Any(m => m.Id != dto.Id &&
            (StringValidator.CompareString(m.CorporateName, dto.CorporateName) ||
            StringValidator.CompareString(m.TradeName, dto.TradeName)));
    }
}
