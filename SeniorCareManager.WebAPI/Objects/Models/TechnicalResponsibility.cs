using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SeniorCareManager.WebAPI.Objects.Models
{
    [Table("technicalResponsibility")]
    public class TechnicalResponsibility
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("responsibleName")]
        public string ResponsibleName { get; set; }

        [Column("professionalRegistration")]
        public string ProfessionalRegistration { get; set; }

        [Column("servicesResponsibility")]
        public string ServicesResponsibility { get; set; }

        [Column("startDate")]
        public DateTime StartDate { get; set; }

        [Column("endDate")]
        public DateTime EndDate { get; set; }
        /*
        [Column("company_Id"), ForeignKey("Company")]
        public int? CompanyId { get; set; }
        [JsonIgnore]
        public virtual Company? Company { get; set; }
        */
        [Column("employee_Id"), ForeignKey("Employee")]
        public int? EmployeeId { get; set; }

        [JsonIgnore]
        public virtual Employee? Employee { get; set; }


        [Column("position_id"), ForeignKey("Position")]
        public int PositionId { get; set; }

        [JsonIgnore]
        public virtual Position? Position { get; set; }

        public TechnicalResponsibility () { }

        public TechnicalResponsibility (int id, string responsibleName, string professionalRegistration, string servicesResponsibility, DateTime startDate, DateTime endDate, int positionId, int employeeId /*,int companyId*/) 
        {
            Id = id;
            ResponsibleName = responsibleName;
            ProfessionalRegistration = professionalRegistration;
            ServicesResponsibility = servicesResponsibility;
            StartDate = startDate;
            EndDate = endDate;
            PositionId = positionId;
            EmployeeId = employeeId;
            //CompanyId = companyId;
        }
    }
}
