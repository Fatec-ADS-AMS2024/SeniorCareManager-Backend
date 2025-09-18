using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;
using System.Text.RegularExpressions;

namespace SeniorCareManager.WebAPI.Services.Utils
{
    public static class StringValidator
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
            return string.Equals(RemoveDiacritics(str1), RemoveDiacritics(str2), StringComparison.OrdinalIgnoreCase);
        }
        // verifique se o registro possui dependentes relacionados (relação um-para-muitos). A exclusão só deve ocorrer se não houver dependentes.

            if (itemId == currentId)
                continue;

            if (CompareString(nameSelector(item), currentName))
                return true;
        }

        return false;
    }
}