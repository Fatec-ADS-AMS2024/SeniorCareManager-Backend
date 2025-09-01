using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class DateValidator : BaseAnnotation
{
    public DateValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null || parameters.Length == 0)
            throw new ArgumentNullException("Essa função precisa de parâmetros: mínimo e/ou máximo.");
    }

    public override void Execute()
    {
        if (Value is not DateTime date)
        {
            ReturnError("Valor informado não é uma data válida.");
            return;
        }

        DateTime? minDate = null;
        DateTime? maxDate = null;

        foreach (var param in Parameters)
        {
            if (param is DateTime dt)
            {
                if (minDate is null) minDate = dt;
                else maxDate = dt;
            }
        }

        if (minDate.HasValue && date < minDate.Value)
        {
            ReturnError($"A data não pode ser anterior a {minDate:dd/MM/yyyy}.");
            return;
        }

        if (maxDate.HasValue && date > maxDate.Value)
        {
            ReturnError($"A data não pode ser posterior a {maxDate:dd/MM/yyyy}.");
            return;
        }
    }
}