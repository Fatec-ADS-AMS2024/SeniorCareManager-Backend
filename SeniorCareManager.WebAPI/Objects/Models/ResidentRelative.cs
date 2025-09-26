using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    public class ResidentRelative
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("residentId")]
        public int ResidentId { get; set; }

        public Resident Resident { get; set; }

        [Column("relationship")]
        public Relationship Relationship { get; set; } 

        [Column("name")]
        public string Name { get; set; }

        [Column("citizenship")]
        public string Citizenship { get; set; }

        [Column("mobileNumber")]
        public string MobileNumber { get; set; }

        [Column("homePhoneNumber")]
        public string HomePhoneNumber { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("street")]
        public string Street { get; set; }

        [Column("number")]
        public string Number { get; set; }

        [Column("addressComplement")]
        public string AddressComplement { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("state")]
        public string State { get; set; }

        [Column("postalCode")]
        public string PostalCode { get; set; }

        [Column("issuingBody")]
        public string IssuingBody { get; set; }

        public ResidentRelative() { }   

        public ResidentRelative(int id, int residentId, Relationship relationship, string name, string citizenship, string mobileNumber, string homePhoneNumber, string email, string street, string number, string addressComplement, string city, string state, string postalCode, string issuingBody)
        {
            Id = id;
            ResidentId = residentId;
            Relationship = relationship;
            Name = name;
            Citizenship = citizenship;
            MobileNumber = mobileNumber;
            HomePhoneNumber = homePhoneNumber;
            Email = email;
            Street = street;
            Number = number;
            AddressComplement = addressComplement;
            City = city;
            State = state;
            PostalCode = postalCode;
            IssuingBody = issuingBody;
        }
    }
}