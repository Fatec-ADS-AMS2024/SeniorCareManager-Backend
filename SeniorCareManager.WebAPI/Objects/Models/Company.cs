using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    [Table("company")]
    public class Company
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("companyname")]
        public string CompanyName { get; set; }

        [Column("tradename")]
        public string TradeName { get; set; }

        [Column("cnpj")]
        public string CNPJ { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("number")]
        public string Number { get; set; }

        [Column("district")]
        public string District { get; set; }

        [Column("addresscomplement")]
        public string AddressComplement { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("postalcode")]
        public string PostalCode { get; set; }

        [Column("companylogo")]
        public string CompanyLogo { get; set; }

        public Company() { }

        public Company(int id, string companyName, string tradeName, string cnpj, string email, string street, string number, string district, string addressComplement, string city, string state, string postalCode, string companyLogo)
        {
            Id = id;
            CompanyName = companyName;
            TradeName = tradeName;
            CNPJ = cnpj;
            Email = email;
            Street = street;
            Number = number;
            District = district;
            AddressComplement = addressComplement;
            City = city;
            State = state;
            PostalCode = postalCode;
            CompanyLogo = companyLogo;
        }
    }
}
