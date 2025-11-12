using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class UpperCaracters : BaseAnnotation
{
    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        string valor = Value?.ToString().ToUpper();

        Value = valor;

        return null;
    }
}
