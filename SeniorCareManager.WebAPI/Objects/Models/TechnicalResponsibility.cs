using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public int? CompanyId { get; set; }
        [JsonIgnore]
        public Company? Company { get; set; }

        public int? EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }
        */
        public int? PositionId { get; set; }
        [JsonIgnore]
        public Position? Position { get; set; }

        public TechnicalResponsibility () { }

        public TechnicalResponsibility (int id, string responsibleName, string professionalRegistration, string servicesResponsibility, DateTime startDate, DateTime endDate, int positionId /* , int companyId, int employeeId */ ) 
        {
            Id = id;
            ResponsibleName = responsibleName;
            ProfessionalRegistration = professionalRegistration;
            ServicesResponsibility = servicesResponsibility;
            StartDate = startDate;
            EndDate = endDate;
            PositionId = positionId;
            //EmployeeId = employeeId;
            //CompanyId = companyId;
        }
    }
}
