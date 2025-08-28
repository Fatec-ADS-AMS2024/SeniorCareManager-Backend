using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using System.Text.RegularExpressions;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class EmailValidator : BaseAnnotation
{
    public EmailValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    private static readonly Regex _emailRegex = new Regex(
    @"^[\w\.-]+@[\w\.-]+\.\w{2,}$",
    RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static bool IsValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return _emailRegex.IsMatch(email);
    }
    public override void Execute()
    {
        string valor = Value?.ToString();

        if (string.IsNullOrWhiteSpace(valor))
            ReturnError("O campo não pode ser nulo ou vazio.");

        if (!IsValid(valor))
            ReturnError("Email inválido.");

    }
}
