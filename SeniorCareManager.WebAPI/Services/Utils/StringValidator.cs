using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SeniorCareManager.WebAPI.Services.Utils;

public static class StringValidator
{
    public static string RemoveDiacritics(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    public static string ExtractNumbers(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return new string(text.Where(char.IsDigit).ToArray());
    }

    public static bool CompareString(string str1, string str2)
    {
        if (str1 == null || str2 == null) return false;

        str1 = str1.Trim().RemoveDiacritics();
        str2 = str2.Trim().RemoveDiacritics();

        return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
    }

    public static bool ContainsOnlyLettersAndSpaces(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
    }

    public static bool ContainsOnlyLettersNumbersSpaces(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && value.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
    }

    public static bool StartsWithLetter(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && char.IsLetter(value[0]);
    }

    public static bool IsAlphabetic(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && value.All(char.IsLetter);
    }

    public static bool IsNumeric(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && value.All(char.IsDigit);
    }

    public static bool IsAlphanumeric(string value)
    {
        return !string.IsNullOrWhiteSpace(value) && value.All(char.IsLetterOrDigit);
    }

    public static bool HasExactLength(string value, int length)
    {
        return !string.IsNullOrWhiteSpace(value) && value.Length == length;
    }

    public static bool IsLengthBetween(string value, int min, int max)
    {
        return !string.IsNullOrWhiteSpace(value) && value.Length >= min && value.Length <= max;
    }

    public static bool IsValidUF(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return false;

        string[] ufs = {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
            "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
            "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        return ufs.Contains(value.ToUpper());
    }
}
