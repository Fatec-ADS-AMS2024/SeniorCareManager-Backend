using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class ResidentDTO
    {
        public int Id { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string RegisteredName { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string SocialName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string Age { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string Cpf { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string Rg { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string IssuingBody { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string IssuingState { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string PisPasep { get; set; }

        public Sex Sex { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Ethnicity Ethnicity { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string FatherName { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string MotherName { get; set; }

        [RemoveSpaces]
        public string SpouseName { get; set; }

        [RemoveSpaces]
        public string NationalHealthCardNumber { get; set; }

        [RemoveSpaces]
        public string PrivateHealthCardNumber { get; set; }

        [RemoveSpaces]
        public string MobileNumber { get; set; }

        [RemoveSpaces]
        public string HomePhoneNumber { get; set; }

        [NumValidator(0.0, ErrorMessage ="O tamannho não pode ser negativo.")]
        public decimal Height { get; set; }

        [NumValidator(0.0, ErrorMessage = "O peso não pode ser negativo.")]
        public decimal Weight { get; set; }
    }
}
