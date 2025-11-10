using System;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class CepValidator : BaseAnnotation
{

    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;
        var cep = Value as string;

        var digitsOnly = new string(cep?.Where(char.IsDigit).ToArray());

        if (digitsOnly.Length != 8)
        {
            return ReturnError(NameProperty, "CEP inválido.");
        }
        
        return null;
    }
}
