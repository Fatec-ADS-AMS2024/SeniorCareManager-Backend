using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
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
                throw new ExceptionNotFound($"Empresa com o id {id} informado não foi encontrada.");

            return _mapper.Map<CompanyDTO>(company);
        }

        public override async Task Create(CompanyDTO companyDto)
        {
            if (companyDto is null)
                throw new ExceptionBadRequest("Empresa não pode ser nula.");

            if (await CheckDuplicates(companyDto))
                throw new ExceptionConflict("Nome corporativo ou nome comercial duplicado.");

            if (await _companyRepository.GetById(companyDto.Id) is not null)
                return;

            await base.Create(companyDto);
            await _companyRepository.SaveChanges();
        }

        public override async Task Update(CompanyDTO companyDto, int id)
        {
            if (companyDto is null)
                throw new ExceptionBadRequest("Empresa não pode ser nula.");

            if (await CheckDuplicates(companyDto))
                throw new ExceptionConflict("Nome corporativo ou nome comercial duplicado.");

            if (companyDto.Id != id)
                throw new ExceptionBadRequest("O id da empresa dever ser o mesmo.");

            await base.Update(companyDto, id);
        }

        public async Task UpdateLogo(CompanyDTO dto)
        {
            if (dto is null || dto.CompanyLogo == null || dto.CompanyLogo.Length == 0)
                throw new ExceptionBadRequest("Logo inválido ou vazio.");

            var company = await _companyRepository.GetById(dto.Id)
                ?? throw new ExceptionNotFound($"Empresa com o id {dto.Id} não foi encontrada.");

            company.CompanyLogo = dto.CompanyLogo;

            try
            {
                await _companyRepository.Update(company);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateLogo(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ExceptionBadRequest("Arquivo de logo inválido ou vazio.");

            var company = await _companyRepository.GetById(id)
                ?? throw new ExceptionNotFound($"Empresa com o id {id} não foi encontrada.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var bytes = ms.ToArray();

            if (bytes.Length == 0)
                throw new ExceptionBadRequest("Não foi possível ler o conteúdo do logo.");

            company.CompanyLogo = bytes;
            await _companyRepository.Update(company);
        }

        public async Task<(string Base64, string? MimeType)> GetLogoBase64(int id, string? mimeType = null)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null || company.CompanyLogo is null || company.CompanyLogo.Length == 0)
                return (string.Empty, mimeType ?? "image/png");

            mimeType ??= "image/png";
            var base64 = Convert.ToBase64String(company.CompanyLogo);
            return (base64, mimeType);
        }

        public override async Task Remove(int id)
        {
            var company = await _companyRepository.GetById(id);
            if (company is null)
                throw new ExceptionNotFound($"Empresa com o id {id} informado não foi encontrada.");

            await base.Remove(id);
        }

        public async Task<bool> CheckDuplicates(CompanyDTO dto)
        {
            var companies = await _companyRepository.Get();
            return companies.Any(m => m.Id != dto.Id &&
                (StringUtils.CompareString(m.CompanyName, dto.CompanyName) ||
                 StringUtils.CompareString(m.TradeName, dto.TradeName)));
        }
    }
}
