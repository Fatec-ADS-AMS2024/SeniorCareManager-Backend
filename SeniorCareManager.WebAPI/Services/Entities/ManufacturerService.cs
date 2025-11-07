using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Services.Entities
{
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
                throw new ExceptionBadRequest("Fabricante com o id " + id + " informado não foi encontrado.");

            return _mapper.Map<ManufacturerDTO>(manufacturer);
        }

        public override async Task<ManufacturerDTO> Create(ManufacturerDTO manufacturerDto)
        {
            if (manufacturerDto is null)
                throw new ExceptionBadRequest("O Fabricante não pode ser nulo.");

            if (await CheckDuplicates(manufacturerDto))
                throw new ExceptionConflict("Dados duplicados (Razão social, Nome comercial ou CPF/CNPJ).");

            return _mapper.Map<ManufacturerDTO>(await base.Create(manufacturerDto));
        }

        public override async Task Update(ManufacturerDTO manufacturerDto, int id)
        {
            var errors = new List<FieldError>();

            if (manufacturerDto is null)
                throw new ExceptionBadRequest("O Fabricante não pode ser nulo.");

            if (manufacturerDto.Id != id)
                throw new ExceptionBadRequest("O id do Fabricante deve ser o mesmo.");

            // Verificação de duplicidades campo a campo para dar feedback detalhado
            var manufacturers = await _manufacturerRepository.Get();
            bool corporateDup = manufacturers.Any(m =>
                m.Id != id && StringUtils.CompareString(m.CorporateName, manufacturerDto.CorporateName));
            bool tradeDup = manufacturers.Any(m =>
                m.Id != id && StringUtils.CompareString(m.TradeName, manufacturerDto.TradeName));
            bool docDup = manufacturers.Any(m =>
                m.Id != id && StringUtils.CompareString(m.CpfCnpj, manufacturerDto.CpfCnpj));

            if (corporateDup)
                errors.Add(new FieldError { Field = "CorporateName", Message = "Razão social já existente." });
            if (tradeDup)
                errors.Add(new FieldError { Field = "TradeName", Message = "Nome comercial já existente." });
            if (docDup)
                errors.Add(new FieldError { Field = "CpfCnpj", Message = "CPF/CNPJ já existente." });

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

        private async Task<bool> CheckDuplicates(ManufacturerDTO dto)
        {
            var manufacturers = await _manufacturerRepository.Get();
            return manufacturers.Any(m =>
                StringUtils.CompareString(m.CorporateName, dto.CorporateName) ||
                StringUtils.CompareString(m.TradeName, dto.TradeName) ||
                StringUtils.CompareString(m.CpfCnpj, dto.CpfCnpj)
            );
        }
    }
}