using System;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class EnumValidator : BaseAnnotation
{
    private readonly Type _enumType;

    public EnumValidator(Type enumType)
    {
        if (enumType == null || !enumType.IsEnum)
        {
            throw new ArgumentException("O tipo fornecido deve ser um Enum.");
        }
        _enumType = enumType;
    }

    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;
        bool isValid;

        if (Value is string stringValue)
        {
            isValid = Enum.TryParse(_enumType, stringValue, true, out _);
        }
        else
        {
            isValid = Enum.IsDefined(_enumType, Value);
        }

        if (!isValid)
        {
            return ReturnError(NameProperty, "Valor inválido.");
        }

        return null;
    }
}
