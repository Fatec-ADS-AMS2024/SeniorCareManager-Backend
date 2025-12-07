using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class TechnicalResponsibilityDTO
    {
        public int Id { get; set; }

        [RequiredValidator(ErrorMessage = "O Nome do Responsável não pode ser nulo.")]
        [RemoveSpaces]
        public string ResponsibleName { get; set; }

        [RequiredValidator(ErrorMessage = "O Registro Profissional não pode ser nulo.")]
        [RemoveSpaces]
        public string ProfessionalRegistration { get; set; }

        [RequiredValidator(ErrorMessage = "A Responsabilidade de Serviços não pode ser nula.")]
        [RemoveSpaces]
        public string ServicesResponsibility { get; set; }

        [DateRangeValidator("2000-01-01", ErrorMessage = "Data inicial inválida.")]
        [RequiredValidator(ErrorMessage = "A data inical é obrigatória.")]
        public DateTime StartDate { get; set; }

        [DateRangeValidator("2000-01-01", ErrorMessage = "Data final inválida.")]
        public DateTime EndDate { get; set; }
        public int PositionId { get; set; }
        public int? EmployeeId { get; set; }
        //public int CompanyId { get; set; }
    }
}
