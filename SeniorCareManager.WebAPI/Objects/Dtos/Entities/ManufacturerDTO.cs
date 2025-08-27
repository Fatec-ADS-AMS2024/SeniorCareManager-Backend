using SeniorCareManager.WebAPI.Services.Utils;

public class ManufacturerDTO
{
    public int Id { get; set; }

    private string _corporatename;
    private string _tradename;
    private string _cpfCnpj;
    private string _phone;
    private string _email; 

    public string CorporateName
    {
        get => _corporatename;
        set => _corporatename = value.Trim();
    }

    public string TradeName
    {
        get => _tradename;
        set => _tradename = value.Trim();
    }
    // Formatação do CPF/CNPJ
    public string CpfCnpj
    {
        get => _cpfCnpj;
        set
        {
            if (CpfCnpjValidator.IsValidCNPJ(value))
            {
                _cpfCnpj = value.Trim();
            }
            else
            {
                _cpfCnpj = null;
            }
        }
    }

    // Formatação do telefone
    public string Phone
    {
        get => _phone;
        set
        {
            if (PhoneValidator.IsValidPhoneNumber(value))
            {
                _phone = value.Trim();
            }
        }
    }

    // Validação de email
    public string Email
    {
        get => _email;
        set
        {
            if (EmailValidator.IsValid(value))
            {
                _email = value.Trim();
            }
            else
            {
                _email = null;
            }
        }
    }

    public static bool IsFilledString(params string[] parametros)
    {
         foreach (var parametro in parametros)
         {
           if (string.IsNullOrWhiteSpace(parametro))
           {
            return false;
           }
         }
       return true;
    }
}
