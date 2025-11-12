using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;
using System.Globalization; // Adicione este using

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class DateRangeValidator : BaseAnnotation
{
    private readonly DateTime _minDate;
    private readonly DateTime _maxDate;

    private readonly string _minDateString;
    private readonly string _maxDateString;

    public DateRangeValidator(string? minDate = null, string? maxDate = null)
    {
        if (!string.IsNullOrEmpty(minDate) && DateTime.TryParse(minDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedMinDate))
        {
            _minDate = parsedMinDate;
            _minDateString = _minDate.ToString("dd/MM/yyyy");
        }
        else
        {
            _minDate = DateTime.MinValue;
            _minDateString = "data mínima";
        }

        if (!string.IsNullOrEmpty(maxDate) && DateTime.TryParse(maxDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedMaxDate))
        {
            _maxDate = parsedMaxDate;
            _maxDateString = _maxDate.ToString("dd/MM/yyyy");
        }
        else
        {
            _maxDate = DateTime.MaxValue;
            _maxDateString = "data máxima";
        }
    }

    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        if (Value is not DateTime date)
        {
            if (Value is string dateString && DateTime.TryParse(dateString, out date))
            {
            }
            else
            {
                return ReturnError(NameProperty, "Valor informado não é uma data válida.");
            }
        }

        if (date < _minDate || date > _maxDate)
        {
            return ReturnError(NameProperty, $"A data deve estar entre {_minDateString} e {_maxDateString}.");
        }

        return null;
    }
}
