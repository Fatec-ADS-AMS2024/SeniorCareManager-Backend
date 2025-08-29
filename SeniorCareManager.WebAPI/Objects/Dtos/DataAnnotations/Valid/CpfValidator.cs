using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using static System.Net.Mime.MediaTypeNames;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
public class CpfValidator : BaseAnnotation
{
    public CpfValidator(params object[]? parameters) : base(parameters)
    {
        if (parameters is null)
            throw new ArgumentNullException("Essa funcão precisa de parâmetros");
    }
    public override void Execute()
    {
        string cpf = string.Concat(Value.ToString().Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
            ReturnError("CPF inválido.");

        var digits = cpf.Select(c => c - '0').ToArray();

        int sum1 = 0;
        for (int i = 0; i < 9; i++)
            sum1 += digits[i] * (10 - i);

        int check1 = sum1 % 11;
        check1 = check1 < 2 ? 0 : 11 - check1;

        if (digits[9] != check1)
            ReturnError("CPF inválido.");

        int sum2 = 0;
        for (int i = 0; i < 10; i++)
            sum2 += digits[i] * (11 - i);

        int check2 = sum2 % 11;
        check2 = check2 < 2 ? 0 : 11 - check2;

        if (digits[10] == check2)
            cpf = check2.ToString();
    }
}