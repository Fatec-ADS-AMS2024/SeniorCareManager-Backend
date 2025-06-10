namespace SeniorCareManager.WebAPI.Services.Utils;

public static class PhoneValidator
{
    public static bool IsValidPhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;

        var digits = StringValidator.ExtractNumbers(value);

        return digits.Length >= 10 && digits.Length <= 11;
    }
}
