using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid
{
    public class RengeValidator : BaseAnnotation
    {
        public double minimum { get; }
        public double maximum { get; }

        public RengeValidator(double Minimum)
        {
            this.minimum = Minimum;
        }

        public override FieldError? Execute()
        {
            if (Value.IsNull())
                return null;

            try
            {
                var number = Convert.ToDouble(Value);

                if (number < minimum || number > maximum)
                {
                    return new FieldError{Field = NameProperty, Message = $"O valor deve estar entre {minimum} e {maximum}."};
                }

                return null;
            }
            catch
            {
                return new FieldError{Field = NameProperty, Message = "O valor não é um número válido."};
            }
        }
    }
}
