using System.Numerics;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string TradeName { get; set; }
        public string CNPJ { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string AddressComplement { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CompanyLogo { get; set; }

        public bool CheckName()
        {
            if (string.IsNullOrWhiteSpace(CompanyName) || string.IsNullOrWhiteSpace(TradeName))
                return false;
            return true;
        }

        public bool CheckCpfCnpj()
        {
            if (string.IsNullOrWhiteSpace(CNPJ))
                return false;
            return CNPJ.Length == 14;
        }

        public bool CheckEmail()
        {
            if (string.IsNullOrWhiteSpace(Email))
                return false;
            var addr = new System.Net.Mail.MailAddress(Email);
            return addr.Address == Email;
        }

        public bool CheckPostalCode()
        {
            if (string.IsNullOrWhiteSpace(PostalCode))
                return false;
            return PostalCode.Length == 8 && BigInteger.TryParse(PostalCode, out _);
        }
    }
}
