using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;
using System.Text.RegularExpressions;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class EmailValidator : BaseAnnotation
{

    private static readonly Regex _emailRegex = new Regex(
        @"^(?i)[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        string? valor = Value?.ToString();

        if (valor is not null && !_emailRegex.IsMatch(valor))
            return ReturnError(NameProperty, "Email inválido.");

        return null;
    }
}
