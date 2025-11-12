using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class RemoveSpaces : BaseAnnotation
{
    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        string valor = Value?.ToString().Trim();

        Value = valor;

        return null;
    }
}
