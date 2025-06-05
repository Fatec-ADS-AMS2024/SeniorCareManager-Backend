using SeniorCareManager.WebAPI.Objects.Enums;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Cpf { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime HireDate { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Cep { get; set; }

        public int Number { get; set; }

        public string Neighborhood { get; set; }

        public StatusEmployee StatusEmployee { get; set; }

        public int PositionId { get; set; }
    }
}
