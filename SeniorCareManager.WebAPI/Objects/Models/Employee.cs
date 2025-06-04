using SeniorCareManager.WebAPI.Objects.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Models
{
	[Table("employee")]
	public class Employee
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		public string Name { get; set; }

		[Column("cpf")]
		public string Cpf { get; set; }

		[Column("phone")]
		public string Phone { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("hiredate")]
		public DateTime HireDate { get; set; }

		[Column("state")]
		public string State { get; set; }

		[Column("city")]
		public string City { get; set; }

		[Column("street")]
		public string Street { get; set; }

		[Column("cep")]
		public string Cep { get; set; }

		[Column("number")]
		public int Number { get; set; }

		[Column("neighborhood")]
		public string Neighborhood { get; set; }

		[Column("status_employee")]
		public StatusEmployee StatusEmployee { get; set; }

		[Column("position_id")]
		public int PositionId { get; set; }

		[ForeignKey("PositionId")]
		public Position Position { get; set; }

		public Employee() { }

		public Employee(
			int id,
			string name,
			string cpf,
			string phone,
			string email,
			DateTime hireDate,
			string state,
			string city,
			string street,
			string cep,
			int number,
			string neighborhood,
			StatusEmployee statusEmployee,
			int positionId
		)
		{
			Id = id;
			Name = name;
			Cpf = cpf;
			Phone = phone;
			Email = email;
			HireDate = hireDate;
			State = state;
			City = city;
			Street = street;
			Cep = cep;
			Number = number;
			Neighborhood = neighborhood;
			StatusEmployee = statusEmployee;
			PositionId = positionId;
		}
	}
}
