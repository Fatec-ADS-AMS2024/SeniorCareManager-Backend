using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    public class Resident
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("registeredName")]
        public string RegisteredName { get; set; }
        [Column("socialName")]
        public string SocialName { get; set; }
        [Column("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [Column("age")]
        public string Age { get; set; }
        [Column("cpf")]
        public string Cpf { get; set; }
        [Column("rg")]
        public string Rg { get; set; }
        [Column("issuingBody")]
        public string IssuingBody { get; set; }
        [Column("issuingState")]
        public string IssuingState { get; set; }
        [Column("pisPasep")]
        public string PisPasep { get; set; }
        [Column("sex")]
        public Sex Sex { get; set; }
        [Column("maritalStatus")]
        public MaritalStatus MaritalStatus { get; set; }
        [Column("ethnicity")]
        public Ethnicity Ethnicity { get; set; }
        [Column("fatherName")]
        public string FatherName { get; set; }
        [Column("motherName")]
        public string MotherName { get; set; }
        [Column("spouseName")]
        public string SpouseName { get; set; }
        [Column("nationalHealthCardNumber")]
        public string NationalHealthCardNumber { get; set; }
        [Column("privateHealthCardNumber")]
        public string PrivateHealthCardNumber { get; set; }
        [Column("mobileNumber")]
        public string MobileNumber { get; set; }
        [Column("homePhoneNumber")]
        public string HomePhoneNumber { get; set; }
        [Column("height")]
        public decimal Height { get; set; }
        [Column("weight")]
        public decimal Weight { get; set; }

        // Navegações para relacionamentos 1:N
        public ICollection<ResidentRelative> Relatives { get; set; } = new List<ResidentRelative>();
        public ICollection<ResidentAllergy> Allergies { get; set; } = new List<ResidentAllergy>();

        public Resident() { }

        public Resident(int id, string registeredName, string socialName, DateTime dateOfBirth, string age, string cpf, string rg, string issuingBody, string issuingState, string pisPasep, Sex sex, MaritalStatus maritalStatus, Ethnicity ethnicity, string fatherName, string motherName, string spouseName, string nationalHealthCardNumber, string privateHealthCardNumber, string mobileNumber, string homePhoneNumber, decimal height, decimal weight)
        {
            Id = id;
            RegisteredName = registeredName;
            SocialName = socialName;
            DateOfBirth = dateOfBirth;
            Age = age;
            Cpf = cpf;
            Rg = rg;
            IssuingBody = issuingBody;
            IssuingState = issuingState;
            PisPasep = pisPasep;
            Sex = sex;
            MaritalStatus = maritalStatus;
            Ethnicity = ethnicity;
            FatherName = fatherName;
            MotherName = motherName;
            SpouseName = spouseName;
            NationalHealthCardNumber = nationalHealthCardNumber;
            PrivateHealthCardNumber = privateHealthCardNumber;
            MobileNumber = mobileNumber;
            HomePhoneNumber = homePhoneNumber;
            Height = height;
            Weight = weight;
        }
    }
}