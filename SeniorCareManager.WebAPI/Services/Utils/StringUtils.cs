using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace SeniorCareManager.WebAPI.Services.Utils
{
    public static class StringUtils
    {
<<<<<<< HEAD
        //Deixa os nomes iguais para depois ver se tem duplicados
=======
>>>>>>> 9acafb3e03fff0ba3a9956f0e48de3c8625b3d62
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
<<<<<<< HEAD

        public static string ExtractNumbers(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return new string(text.Where(char.IsDigit).ToArray());
        }
=======
>>>>>>> 9acafb3e03fff0ba3a9956f0e48de3c8625b3d62
        public static bool CompareString(string str1, string str2)
        {
            return string.Equals(str1.RemoveDiacritics(), str2.RemoveDiacritics(), StringComparison.OrdinalIgnoreCase);
        }
<<<<<<< HEAD
        // verifique se o registro possui dependentes relacionados (relação um-para-muitos). A exclusão só deve ocorrer se não houver dependentes.



    }
}
=======

    }
}
>>>>>>> 9acafb3e03fff0ba3a9956f0e48de3c8625b3d62
