using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities;

public class ReligionService : GenericService<Religion, ReligionDTO>, IReligionService
{
    private readonly IReligionRepository _religionRepository;
    private readonly IMapper _mapper;

    public ReligionService(IReligionRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _religionRepository = repository;
        _mapper = mapper;
    }
    public override async Task<ReligionDTO> GetById(int id)
    {
        var religion = await _religionRepository.GetById(id);
        if (religion is null)
            throw new ExceptionBadRequest("Religião com o id " + id + " informado não foi encontrada.");

        return _mapper.Map<ReligionDTO>(religion);
    }
    public override async Task<ReligionDTO> Create(ReligionDTO religionDto)
    {
       

        if (religionDto is null)
            throw new ExceptionBadRequest("A Religião não pode ser nula.");

        if (await CheckDuplicates(religionDto.Name))
            throw new ExceptionConflict("Nome já existente.");


        return _mapper.Map<ReligionDTO>( await base.Create(religionDto) );
    }
    public override async Task Update(ReligionDTO religionDto, int id)
    {
        if (religionDto is null)
            throw new ExceptionBadRequest("A Religião não pode ser nula.");

        if (religionDto.id != id)
            throw new ExceptionBadRequest("O id da religião dever ser o mesmo.");

        if (await CheckDuplicates(religionDto.Name))
            throw new ExceptionConflict("Nome já existente.");

        await base.Update(religionDto, id);
    }
    public override async Task Remove(int id)
    {
        var religion = await _religionRepository.GetById(id);
        if (religion is null)
            throw new ExceptionBadRequest("Religião com o id " + id + " informado não foi encontrada.");

        await base.Remove(id);
    }
    public async Task<bool> CheckDuplicates(string nome)
    {
        var religions = await _religionRepository.Get();
        return religions.Any(r => StringUtils.CompareString(r.Name, nome));
    }
}