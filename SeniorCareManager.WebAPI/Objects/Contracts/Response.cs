using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Services.Utils
{
    public class Response
    {
        public ResponseEnum Code { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
