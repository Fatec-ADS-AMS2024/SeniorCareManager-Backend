using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class UfValidator : BaseAnnotation
{
    public UfValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override void Execute()
    {
        string uf = Value?.ToString().ToUpper();

        string[] ufs = {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
            "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
            "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        if (!ufs.Contains(uf))
            ReturnError($"UF inválida: '{uf}' não é reconhecida como uma unidade federativa brasileira.");

    }
}
