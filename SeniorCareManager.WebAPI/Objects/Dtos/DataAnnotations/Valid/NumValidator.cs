using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class NumValidator : BaseAnnotation
{
    public NumValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null || parameters.Length == 0)
            throw new ArgumentNullException("Essa função precisa de parâmetros.");
    }

    public override void Execute()
    {
        if (!decimal.TryParse(Value.ToString(), out var valor))
        {
            ReturnError("O valor informado não é numérico.");
            return;
        }

        if (!decimal.TryParse(Parameters[0].ToString(), out var minValue))
        {
            ReturnError("O parâmetro mínimo é inválido.");
            return;
        }

        if (valor < minValue)
        {
            ReturnError($"O valor {valor} é menor que o mínimo permitido ({minValue}).");
        }

    }
}