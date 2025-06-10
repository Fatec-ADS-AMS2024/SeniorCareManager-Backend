using System.Text.RegularExpressions;

namespace SeniorCareManager.WebAPI.Services.Utils;
public static class EmailValidator
{
    private static readonly Regex _emailRegex = new Regex(
        @"^[\w\.-]+@[\w\.-]+\.\w{2,}$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static bool IsValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return _emailRegex.IsMatch(email);
    }
}