using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class DateValidator : BaseAnnotation
{
    public DateValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
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

        foreach (var param in Parameters)
        {
            if (param is DateTime dt)
            {
                minDate = dt;
                break; 
            }
            else if (param is string str && DateTime.TryParse(str, out var parsed))
            {
                minDate = parsed;
                break;
            }
            else
            {
                ReturnError("Parâmetro inválido para data mínima.");
                return;
            }
        }

        if (minDate.HasValue && date < minDate.Value)
        {
            ReturnError($"A data não pode ser anterior a {minDate:dd/MM/yyyy}.");
        }
    }
}