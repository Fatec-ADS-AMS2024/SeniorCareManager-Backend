using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class RemoveSpaces : BaseAnnotation
{
    public RemoveSpaces(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override void Execute()
    {
        string valor = Value?.ToString().Trim();

        SetValue(valor);
    }
}