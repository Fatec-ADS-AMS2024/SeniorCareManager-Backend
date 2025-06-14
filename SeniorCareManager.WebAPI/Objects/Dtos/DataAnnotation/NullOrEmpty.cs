using System.ComponentModel.DataAnnotations;

public class NullOrEmptyAttribute : ValidationAttribute
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		// Verifica se o valor é nulo ou vazio
		if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
		{
			return new ValidationResult("O campo não pode estar vazio ou nulo.");
		}
		// Se tudo estiver certo, retorna sucesso
		return ValidationResult.Success;
	}
}
