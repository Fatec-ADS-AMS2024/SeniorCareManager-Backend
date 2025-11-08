using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class QtdCaractersRangeValidator : BaseAnnotation
{
    private readonly int _min;
    private readonly int _max;

    public QtdCaractersRangeValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters == null || parameters.Length < 2)
            throw new ArgumentException("É necessário informar pelo menos dois parâmetros: valor mínimo e máximo.");

        _min = Convert.ToInt32(parameters[0]);
        _max = Convert.ToInt32(parameters[1]);

        if (_min > _max)
            throw new ArgumentException("O valor mínimo não pode ser maior que o máximo.");
    }

    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        var length = Value?.ToString()?.Length ?? 0;

        if (length < _min || length > _max)
            return ReturnError(NameProperty, $"O campo deve conter entre {_min} e {_max} caracteres.");

        return null;
    }
}
