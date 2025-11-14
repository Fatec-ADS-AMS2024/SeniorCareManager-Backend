using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid
{
    public class RengeValidator : BaseAnnotation
    {
        public double minimum { get; }
        public double maximum { get; set; }

        public RengeValidator(double Minimum)
        {
            this.minimum = Minimum;
            this.maximum = double.MaxValue;
        }

        public override FieldError? Execute()
        {
            if (Value.IsNull())
                return null;

            try
            {
                Console.WriteLine();

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
