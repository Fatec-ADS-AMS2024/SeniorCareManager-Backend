using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;
using System; // Necessário para Convert

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class StringLengthValidator : BaseAnnotation
{
    public int Maximum { get; set; } = int.MaxValue;
    public int Minimum { get; set; } = 0;

    public StringLengthValidator() { }

    public StringLengthValidator(int maximum)
    {
        this.Maximum = maximum;
    }

    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        try
        {
            var value = Convert.ToString(Value);

            if (value?.Length < Minimum || value?.Length > Maximum)
            {
                string error = this.ErrorMessage ?? $"O campo deve conter entre {Minimum} e {Maximum} caracteres.";

                return ReturnError(NameProperty, error);
            }
        }
        catch
        {
            return ReturnError(NameProperty, "O campo deve ser uma string.");
        }

        return null;
    }
}
