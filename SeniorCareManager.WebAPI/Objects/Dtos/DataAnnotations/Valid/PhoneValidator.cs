using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using static System.Net.Mime.MediaTypeNames;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class PhoneValidator : BaseAnnotation
{
    public PhoneValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override void Execute()
    {
        string valor = string.Concat(Value.ToString().Where(char.IsDigit));

        if (string.IsNullOrWhiteSpace(valor))
            ReturnError("O campo não pode ser nulo ou vazio.");
        if (valor.Length != 10 && valor.Length != 11)
            ReturnError("Telefone inválido.");
    }
}