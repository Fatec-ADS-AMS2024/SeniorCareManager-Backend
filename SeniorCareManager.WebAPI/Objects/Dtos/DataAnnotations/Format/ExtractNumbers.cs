using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class ExtractNumbers : BaseAnnotation
{


    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        string valor = new string(Value.ToString()?.Where(char.IsDigit).ToArray());

        if (string.IsNullOrWhiteSpace(valor))
            return ReturnError(NameProperty, "O campo não pode ser nulo ou vazio.");
        Value = valor;
        return null;
    }
}
