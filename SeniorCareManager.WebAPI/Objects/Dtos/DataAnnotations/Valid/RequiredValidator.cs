using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class RequiredValidator : BaseAnnotation
{
    public override FieldError? Execute()
    {
        if (!Value.IsNull())
            return null;

        return ReturnError(NameProperty, "O campo não pode ser nulo ou vazio.");

    }
}
