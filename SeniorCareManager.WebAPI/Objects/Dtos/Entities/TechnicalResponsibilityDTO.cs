using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class TechnicalResponsibilityDTO
    {
        public int Id { get; set; }

        [NullOrEmpty("O Nome do Responsável não pode ser nulo.")]
        [RemoveSpaces]
        public string ResponsibleName { get; set; }

        [NullOrEmpty("O Registro Profissional não pode ser nulo.")]
        [RemoveSpaces]
        public string ProfessionalRegistration { get; set; }

        [NullOrEmpty("A Responsabilidade de Serviços não pode ser nula.")]
        [RemoveSpaces]
        public string ServicesResponsibility { get; set; }

        [NullOrEmpty("A Data Inicial não é válida.")]
        [DateValidator("01/01/2000")]
        public DateTime StartDate { get; set; }

        [NullOrEmpty("A Data Final não é válida.")]
        [DateValidator("01/01/2000", "31/12/2030")]
        public DateTime EndDate { get; set; }
    }
}
