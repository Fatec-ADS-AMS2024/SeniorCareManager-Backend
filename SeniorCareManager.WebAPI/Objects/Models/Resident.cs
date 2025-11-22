using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    [Table("resident")]
    public class Resident
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("registered_name")]
        public string RegisteredName { get; set; }

        [Column("social_name")]
        public string? SocialName { get; set; }

        [Column("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [Column("age")]
        public string Age { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("rg")]
        public string Rg { get; set; }

        [Column("issuing_body")]
        public string IssuingBody { get; set; }

        [Column("issuing_state")]
        public string IssuingState { get; set; }

        [Column("pis_pasep")]
        public string? PisPasep { get; set; }

        [Column("sex")]
        public Sex Sex { get; set; }

        [Column("marital_status")]
        public MaritalStatus MaritalStatus { get; set; }

        [Column("ethnicity")]
        public Ethnicity Ethnicity { get; set; }

        [Column("father_name")]
        public string? FatherName { get; set; }

        [Column("mother_name")]
        public string? MotherName { get; set; }

        [Column("spouse_name")]
        public string? SpouseName { get; set; }

        [Column("national_health_card_number")]
        public string? NationalHealthCardNumber { get; set; }

        [Column("private_health_card_number")]
        public string? PrivateHealthCardNumber { get; set; }

        [Column("mobile_number")]
        public string? MobileNumber { get; set; }

        [Column("home_phone_number")]
        public string? HomePhoneNumber { get; set; }

        [Column("height")]
        public decimal? Height { get; set; }

        [Column("weight")]
        public decimal? Weight { get; set; }

        [Column("health_insurance_plan_id")]
        [ForeignKey("HealthInsurancePlan")]
        public int? HealthInsurancePlanId { get; set; }

        [JsonIgnore]
        public virtual HealthInsurancePlan? HealthInsurancePlan { get; set; }

        [Column("religion_id")]
        [ForeignKey("Religion")]
        public int? ReligionId { get; set; }

        [JsonIgnore]
        public virtual Religion? Religion { get; set; }

        [JsonIgnore]
        public virtual ICollection<ResidentRelative> Relatives { get; set; } = new List<ResidentRelative>();

        [JsonIgnore]
        public virtual ICollection<ResidentAllergy> Allergies { get; set; } = new List<ResidentAllergy>();

        public Resident() { }

        public Resident(
            int id,
            string registeredName,
            string? socialName,
            DateTime dateOfBirth,
            string age,
            string cpf,
            string rg,
            string issuingBody,
            string issuingState,
            string? pisPasep,
            Sex sex,
            MaritalStatus maritalStatus,
            Ethnicity ethnicity,
            string? fatherName,
            string? motherName,
            string? spouseName,
            string? nationalHealthCardNumber,
            string? privateHealthCardNumber,
            string? mobileNumber,
            string? homePhoneNumber,
            decimal? height,
            decimal? weight,
            int? healthInsurancePlanId,
            int? religionId)
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
            HealthInsurancePlanId = healthInsurancePlanId;
            ReligionId = religionId;
        }
    }
}
