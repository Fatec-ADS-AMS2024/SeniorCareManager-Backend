using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ResidentRelativeDTO
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        [RemoveSpaces]
        public string Relationship { get; set; }
        [RemoveSpaces]
        public string Name { get; set; }
        [RemoveSpaces]
        public string Citizenship { get; set; }
        [RemoveSpaces]
        public string MobileNumber { get; set; }
        [RemoveSpaces]
        public string HomePhoneNumber { get; set; }
        [RemoveSpaces]
        public string Email { get; set; }
        [RemoveSpaces]
        public string Street { get; set; }
        [RemoveSpaces]
        public string Number { get; set; }
        [RemoveSpaces]
        public string AddressComplement { get; set; }
        [RemoveSpaces]
        public string City { get; set; }
        [RemoveSpaces]
        public string State { get; set; }
        [RemoveSpaces]
        public string PostalCode { get; set; }
        [RemoveSpaces]
        public string IssuingBody { get; set; }
        [RemoveSpaces]
        public string PhoneNumber { get; set; }

    }
}
