namespace SeniorCareManager.WebAPI.Services.Utils;

public static class PhoneValidator
{
    public static bool IsValidPhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;

        var digits = StringValidator.ExtractNumbers(value);

        // Telefones no Brasil têm 10 ou 11 dígitos (com DDD)
        return digits.Length >= 10 && digits.Length <= 11;
    }
}