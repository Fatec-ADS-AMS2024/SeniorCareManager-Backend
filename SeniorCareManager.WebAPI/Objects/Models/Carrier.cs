using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    [Table("carrier")]
    public class Carrier
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("corporate_name")]
        public string CorporateName { get; set; }

        [Column("trade_name")]
        public string TradeName { get; set; }

        [Column("cpf_cnpj")]
        public string CpfCnpj { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("number")]
        public string Number { get; set; }

        [Column("district")]
        public string District { get; set; }

        [Column("address_complement")]
        public string? AddressComplement { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("postal_code")]
        public string PostalCode { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        // public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        public Carrier() { }

        public Carrier(int id, string corporateName, string tradeName, string cpfCnpj, string street, string number, string district, string? addressComplement, string city, string state, string postalCode, string phone, string? email)
        {
            Id = id;
            CorporateName = corporateName;
            TradeName = tradeName;
            CpfCnpj = cpfCnpj;
            Street = street;
            Number = number;
            District = district;
            AddressComplement = addressComplement;
            City = city;
            State = state;
            PostalCode = postalCode;
            Phone = phone;
            Email = email;
        }
    }
}
