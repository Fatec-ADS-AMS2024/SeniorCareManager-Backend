using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class ExtractNumbers : BaseAnnotation
{
    public ExtractNumbers(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public static string ExtractNum(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        return new string(text.Where(char.IsDigit).ToArray());
    }

    public override void Execute()
    {
        string valor = Value?.ToString();

        if (string.IsNullOrWhiteSpace(valor))
            ReturnError("O campo não pode ser nulo ou vazio.");
        valor = valor.ExtractNumbers();
    }
}