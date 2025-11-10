using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class PhoneFormat : BaseAnnotation
{
    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        string valor = new string(Value.ToString()?.Where(char.IsDigit).ToArray());

        if (valor.Length != 10 && valor.Length != 11)
            return ReturnError(NameProperty, "Telefone inválido.");
        if (valor.Length == 11 && valor[2] != '9')
            return ReturnError(NameProperty, "Número de celular inválido.");
        Value = valor;
        return null;
    }
}
