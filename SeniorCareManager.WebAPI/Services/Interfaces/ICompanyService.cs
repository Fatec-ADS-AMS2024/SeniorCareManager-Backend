using SeniorCareManager.WebAPI.Objects.Dtos.Entities;
using SeniorCareManager.WebAPI.Objects.Models;

namespace SeniorCareManager.WebAPI.Services.Interfaces
{
    public interface ICompanyService : IGenericService<Company, CompanyDTO>
    {
        Task<bool> CheckDuplicates(CompanyDTO dto);
        Task UpdateLogo(CompanyDTO dto); 
        Task UpdateLogo(int id, IFormFile file);
        Task<(string Base64, string? MimeType)> GetLogoBase64(int id, string? mimeType = null);
    }
}
