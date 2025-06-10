public class ManufacturerDTO
{
    public int Id { get; set; }
    public string CorporateName { get; set; }
    public string TradeName { get; set; }
    public string CpfCnpj { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public bool CheckName()
    {
        if (string.IsNullOrWhiteSpace(CorporateName)||string.IsNullOrWhiteSpace(TradeName))
            return false;
        return true;
    }

    public bool CheckCpfCnpj()
    {
        if (string.IsNullOrWhiteSpace(CpfCnpj))
            return false;
        return CpfCnpj.Length == 11 || CpfCnpj.Length == 14; // CPF tem 11 dígitos, CNPJ tem 14
    }

    public bool CheckPhone()
    {
        if (string.IsNullOrWhiteSpace(Phone))
            return false;
        return Phone.Length >= 10 && Phone.Length <= 11; // Telefones no Brasil têm 10 ou 11 dígitos (com DDD)
    }

    public bool CheckEmail()
    {
        if (string.IsNullOrWhiteSpace(Email))
            return false;
        var addr = new System.Net.Mail.MailAddress(Email);
        return addr.Address == Email;
    }
}