using SeniorCareManager.WebAPI.Objects.Enums;
using System.ComponentModel.DataAnnotations;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
		
        [NullOrEmpty]
        public string Name { get; set; }

		[NullOrEmpty]
		public string Cpf { get; set; }

		public string Phone { get; set; }
		
        [NullOrEmpty]
		public string Email { get; set; }

        public DateTime HireDate { get; set; }
		[NullOrEmpty]
		public string State { get; set; }

		[NullOrEmpty]
		public string City { get; set; }

		[NullOrEmpty]
		public string Street { get; set; }

        public string Cep { get; set; }

        public int Number { get; set; }

        public string Neighborhood { get; set; }

		public StatusEmployee StatusEmployee { get; set; }

        public int PositionId { get; set; }
    }
}
