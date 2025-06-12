using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
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
            throw new ArgumentNullException("Fabricante com o id " + id + " informado não foi encontrado.");

        return _mapper.Map<ManufacturerDTO>(manufacturer);
    }

    public override async Task Create(ManufacturerDTO manufacturerDto)
    {
        if (manufacturerDto is null)
            throw new ArgumentNullException("O Fabricante não pode ser nulo.");

        if (!ManufacturerDTO.IsFilledString(manufacturerDto.CorporateName) || !ManufacturerDTO.IsFilledString(manufacturerDto.TradeName))
            throw new ArgumentException("Nome corporativo ou nome comercial é inválido.");

        if (await CheckDuplicates(manufacturerDto))
            throw new InvalidOperationException("Nome corporativo ou nome comercial já existente.");

        await base.Create(manufacturerDto);
    }

    public override async Task Update(ManufacturerDTO manufacturerDto, int id)
    {
        if (manufacturerDto is null)
            throw new ArgumentNullException("O Fabricante não pode ser nulo.");

        if (!ManufacturerDTO.IsFilledString(manufacturerDto.CorporateName) || !ManufacturerDTO.IsFilledString(manufacturerDto.TradeName))
            throw new ArgumentException("Nome corporativo ou nome comercial é inválido.");

        if (await CheckDuplicates(manufacturerDto))
            throw new InvalidOperationException("Nome corporativo ou nome comercial já existente.");
        
        if (!manufacturerDto.Id.Equals(id))
            throw new ArgumentException("O id do fabricante não corresponde ao id informado.");



        await base.Update(manufacturerDto, id);
    }

    public override async Task Remove(int id)
    {
        var manufacturer = await _manufacturerRepository.GetById(id);
        if (manufacturer is null)
            throw new ArgumentNullException("Fabricante com o id " + id + " informado não foi encontrado.");

        await base.Remove(id);
    }

    public async Task<bool> CheckDuplicates(ManufacturerDTO dto)
    {
        var manufacturers = await _manufacturerRepository.Get();
        return manufacturers.Any(m => StringUtils.CompareString(m.CorporateName, dto.CorporateName) ||
                                      StringUtils.CompareString(m.TradeName, dto.TradeName));
    }
}
