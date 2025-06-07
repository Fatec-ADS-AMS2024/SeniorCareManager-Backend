using AutoMapper;
using SeniorCareManager.WebAPI.Data.Interfaces;
using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;
using SeniorCareManager.WebAPI.Services.Interfaces;

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

        public async Task Update(int id, CompanyDTO company)
        {
            var existingCompany = await _companyRepository.GetById(id);
            if (existingCompany == null)
            {
                throw new KeyNotFoundException($"Company with ID {id} not found.");
            }

            var updatedCompany = _mapper.Map(company, existingCompany);
            await _companyRepository.Update(updatedCompany);
            await _companyRepository.SaveChanges();
        }
    }
}