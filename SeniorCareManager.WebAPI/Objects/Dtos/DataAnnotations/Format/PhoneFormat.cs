using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class PhoneFormat : BaseAnnotation
{
    public PhoneFormat(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override void Execute()
    {
        string valor = new string(Value.ToString()?.Where(char.IsDigit).ToArray());

        if (valor.Length != 10 && valor.Length != 11)
            ReturnError("Telefone inválido.");
        if (valor.Length == 11 && valor[2] != '9')
            ReturnError("Número de celular inválido.");
        SetValue(valor);
    }
}