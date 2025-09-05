using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace SeniorCareManager.WebAPI.Services.Utils
{
    public static class StringUtils
    {
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
        public static bool CompareString(string str1, string str2)
        {
            return string.Equals(str1.RemoveDiacritics(), str2.RemoveDiacritics(), StringComparison.OrdinalIgnoreCase);
        }

        public static string Clean(string input)
        {
            // 1. Verificação de Segurança
            // Se a string for nula ou vazia, retorna uma string vazia para evitar erros.
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            // 2. A Mágica do Regex
            // Regex.Replace encontra um padrão e o substitui por outra coisa.
            // O padrão @"\D" é uma expressão regular que significa "qualquer caractere que NÃO seja um dígito (0-9)".
            // A substituição "" significa que estamos trocando tudo o que não é número por "nada", efetivamente apagando.
            return Regex.Replace(input, @"\D", "");
        }
    }
}