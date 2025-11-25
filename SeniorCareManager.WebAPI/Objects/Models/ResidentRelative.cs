using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    [Table("resident_relative")]
    public class ResidentRelative
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("rg")]
        public string Rg { get; set; }

        [Column("issuing_state")]
        public string IssuingState { get; set; }

        [Column("relationship")]
        public Relationship Relationship { get; set; }

        [Column("mobile_number")]
        public string MobileNumber { get; set; }

        [Column("home_phone_number")]
        public string HomePhoneNumber { get; set; }

        [Column("email")]
        public string Email { get; set; }

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

        [Column("issuing_body")]
        public string IssuingBody { get; set; }

        [Column("citizenship")]
        public string Citizenship { get; set; }

        [Column("resident_id")]
        public int ResidentId { get; set; }

        [JsonIgnore]
        public virtual Resident? Resident { get; set; }

        public ResidentRelative() { }

        public ResidentRelative(
            int id,
            string name,
            string cpf,
            string rg,
            string issuingState,
            Relationship relationship,
            string mobileNumber,
            string homePhoneNumber,
            string email,
            string street,
            string number,
            string district,
            string? addressComplement,
            string city,
            string state,
            string postalCode,
            string issuingBody,
            string citizenship,
            int residentId)
        {
            Id = id;
            ResidentId = residentId;
            Name = name;
            Cpf = cpf;
            Rg = rg;
            IssuingState = issuingState;
            Relationship = relationship;
            MobileNumber = mobileNumber;
            HomePhoneNumber = homePhoneNumber;
            Email = email;
            Street = street;
            Number = number;
            District = district;
            AddressComplement = addressComplement;
            City = city;
            State = state;
            PostalCode = postalCode;
            IssuingBody = issuingBody;
            Citizenship = citizenship;
        }
    }
}
