using System.Globalization;
using System.Text;

namespace SeniorCareManager.WebAPI.Services.Utils
{
    public static class StringUtils
    {
        //Deixa os nomes iguais para depois ver se tem duplicados
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

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
            return string.Equals(
                SeniorCareManager.WebAPI.Services.Utils.StringUtils.RemoveDiacritics(str1),
                SeniorCareManager.WebAPI.Services.Utils.StringUtils.RemoveDiacritics(str2),
                StringComparison.OrdinalIgnoreCase
            );
        }
    }
}
