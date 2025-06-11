using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Data.Repositories;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;
using SeniorCareManager.WebAPI.Services.Utils;


namespace SeniorCareManager.WebAPI.Services.Entities
{
    public class CompanyService : GenericService<Company, CompanyDTO>, ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _companyRepository = repository;
            _mapper = mapper;
        }

        public override async Task<CompanyDTO> GetById(int id)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new KeyNotFoundException("Empresa com o id " + id + " informado não foi encontrada.");

            return _mapper.Map<CompanyDTO>(company);
        }

        public override async Task Create(CompanyDTO companyDto)
        {
            if (companyDto is null)
                throw new ArgumentNullException(nameof(companyDto), "Empresa não pode ser nula.");

            if (await CheckDuplicates(companyDto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            if (!companyDto.CheckName())
                throw new ArgumentException("Nome Inválido.");

            if (!companyDto.CheckEmail())
                throw new ArgumentException("Email inválido.");

            if (!companyDto.CheckCpfCnpj())
                throw new ArgumentException("CNPJ inválido.");

            if (!companyDto.CheckPostalCode())
                throw new ArgumentException("Código Postal inválido.");


            if (await _companyRepository.GetById(companyDto.Id) is not null)
                return; // Se já existe uma empresa com esse ID, apenas continua

            await base.Create(companyDto);
        }

        public override async Task Update(CompanyDTO companyDto, int id)
        {
            if (companyDto is null)
                throw new ArgumentNullException(nameof(companyDto), "Empresa não pode ser nula.");

            if (await CheckDuplicates(companyDto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            if (!companyDto.CheckName())
                throw new ArgumentException("Nome Inválido.");

            if (!companyDto.CheckEmail())
                throw new ArgumentException("Email inválido.");

            if (!companyDto.CheckCpfCnpj())
                throw new ArgumentException("CNPJ inválido.");


            await base.Update(companyDto, id);
        }

        public override async Task Remove(int id)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new KeyNotFoundException("Empresa com o id " + id + " informado não foi encontrada.");

            await base.Remove(id);
        }
        public async Task<bool> CheckDuplicates(CompanyDTO dto)
        {
            var companies = await _companyRepository.Get();
            return companies.Any(m => m.Id != dto.Id &&
                (StringValidator.CompareString(m.CompanyName, dto.CompanyName) ||
                StringValidator.CompareString(m.TradeName, dto.TradeName)));
        }
    }
}
