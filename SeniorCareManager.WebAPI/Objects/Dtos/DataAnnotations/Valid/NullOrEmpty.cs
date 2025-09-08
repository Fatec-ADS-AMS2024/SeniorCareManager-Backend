using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
public class NullOrEmpty : BaseAnnotation
{
    public NullOrEmpty(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override void Execute()
    {
        string valor = Value?.ToString();

        if (string.IsNullOrWhiteSpace(valor))
            ReturnError("O campo não pode ser nulo ou vazio.");
    }
}