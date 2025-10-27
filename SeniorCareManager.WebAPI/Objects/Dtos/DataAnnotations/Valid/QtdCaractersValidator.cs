using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class QtdCaractersValidator : BaseAnnotation
{

    public QtdCaractersValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parametros");
    }

    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        var qtdValor = Value?.ToString()?.Length;
        if (Parameters != null)
        {
            foreach (var item in Parameters)
            {
                if (qtdValor == (int)item)
                    return null;
            }
        }

        return ReturnError(NameProperty, "Essa quantidade de caracteres não é válida!");

    }
}