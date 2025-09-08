using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class QtdCaractersValidator : BaseAnnotation
{

    public QtdCaractersValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parametros");

    }

    public override void Execute()
    {
        var qtdValor = Value?.ToString()?.Length;
        if (Parameters != null)
        {
            foreach (var item in Parameters)
            {
                if (qtdValor == (int)item)
                    return;
            }
        }

        ReturnError();

    }
}