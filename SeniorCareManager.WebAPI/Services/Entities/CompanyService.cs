using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
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
                throw new KeyNotFoundException($"Empresa com o id {id} informado não foi encontrada.");

            return _mapper.Map<CompanyDTO>(company);
        }

        public override async Task Create(CompanyDTO companyDto)
        {
            if (companyDto is null)
                throw new ArgumentNullException(nameof(companyDto), "Empresa não pode ser nula.");

            if (await CheckDuplicates(companyDto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            if (!companyDto.CheckName())
                throw new ArgumentException("Nome inválido.");

            if (!companyDto.CheckEmail())
                throw new ArgumentException("Email inválido.");

            if (!companyDto.CheckCpfCnpj())
                throw new ArgumentException("CNPJ inválido.");

            if (!companyDto.CheckPostalCode())
                throw new ArgumentException("Código Postal inválido.");

            if (await _companyRepository.GetById(companyDto.Id) is not null)
                return;

            await base.Create(companyDto);
            await _companyRepository.SaveChanges();
        }

        public override async Task Update(CompanyDTO companyDto, int id)
        {
            if (companyDto is null)
                throw new ArgumentNullException(nameof(companyDto), "Empresa não pode ser nula.");

            if (await CheckDuplicates(companyDto))
                throw new InvalidOperationException("Nome corporativo ou nome comercial duplicado.");

            if (!companyDto.CheckName())
                throw new ArgumentException("Nome inválido.");

            if (!companyDto.CheckEmail())
                throw new ArgumentException("Email inválido.");

            if (!companyDto.CheckCpfCnpj())
                throw new ArgumentException("CNPJ inválido.");

            await base.Update(companyDto, id);
        }

        public async Task UpdateLogo(CompanyDTO dto)
        {
            if (dto is null || dto.CompanyLogo == null || dto.CompanyLogo.Length == 0)
                throw new ArgumentException("Logo inválido ou vazio.");

            Company company;

            try
            {
                company = await _companyRepository.GetById(dto.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar empresa: {ex.Message}");
                throw;
            }

            if (company is null)
                throw new KeyNotFoundException($"Empresa com o id {dto.Id} não foi encontrada.");

            company.CompanyLogo = dto.CompanyLogo;

            try
            {
                await _companyRepository.Update(company); // já salva internamente
                Console.WriteLine($"Logo atualizado: {dto.CompanyLogo.Length} bytes");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Erro de concorrência no DbContext: {ex.Message}");
                throw new Exception("Erro interno ao salvar o logo. Tente novamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado ao atualizar logo: {ex.Message}");
                throw;
            }
        }



        public override async Task Remove(int id)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new KeyNotFoundException($"Empresa com o id {id} informado não foi encontrada.");

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
