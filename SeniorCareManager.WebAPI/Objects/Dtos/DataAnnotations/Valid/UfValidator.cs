using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class UfValidator : BaseAnnotation
{
    public override FieldError? Execute()
    {
        if (Value.IsNull())
            return null;

        try
        {
            var uf = Convert.ToString(Value);

            if (uf?.Length != 2)
                return ReturnError(NameProperty, "O campo deve conter 2 letras.");

            uf = uf.ToUpper();

            string[] ufs = {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
                "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
                "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
            };

            if (!ufs.Contains(uf))
                return ReturnError(NameProperty, $"UF inválida: '{uf}' não é reconhecida como uma unidade federativa brasileira.");
        }
        catch
        {
            return ReturnError(NameProperty, "O campo deve ser uma string.");
        }

        return null;
    }
}
