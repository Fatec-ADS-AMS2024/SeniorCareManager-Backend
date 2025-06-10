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
        return string.Equals(str1.RemoveDiacritics(), str2.RemoveDiacritics(), StringComparison.OrdinalIgnoreCase);
    }
    public static bool ContainsDuplicate<T>(
        IEnumerable<T> list,
        Func<T, string> nameSelector,
        string currentName,
        int currentId = 0,
        Func<T, int>? idSelector = null)
    {
        foreach (var item in list)
        {
            var itemId = idSelector?.Invoke(item) ?? 0;

            if (itemId == currentId)
                continue;

            if (CompareString(nameSelector(item), currentName))
                return true;
        }

        return false;
    }
}
