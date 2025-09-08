namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class CompanyLogoDTO
    {
        public int Id { get; set; }
        public byte[] CompanyLogo { get; set; }
        public string? CompanyLogoMimeType { get; set; }
    }
}
