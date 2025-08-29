using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;

public class ManufacturerDTO
{
    public int Id { get; set; }
    public string CorporateName { get; set; }
    public string TradeName { get; set; }
    [CpfValidator]
    public string CpfCnpj { get; set; }
    [PhoneValidator]
    public string Phone { get; set; }
    public string Email { get; set; }
}