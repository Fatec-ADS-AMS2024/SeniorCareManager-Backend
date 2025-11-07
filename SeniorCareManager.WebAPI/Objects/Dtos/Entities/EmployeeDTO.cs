using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using SeniorCareManager.WebAPI.Objects.Enums;
using SeniorCareManager.WebAPI.Objects.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        [NullOrEmpty]
        [RemoveSpaces]
        public string Name { get; set; }

        [RemoveSpaces]
        [NullOrEmpty]
        [CpfCnpjFormat]
        [QtdCaractersValidator(14, "Inserir apenas 14 caracteres.")] 
        public string Cpf { get; set; }

        [NullOrEmpty]
        [RemoveSpaces]
        [QtdCaractersValidator(11, "Inserir apenas 11 caracteres.")]
        [PhoneFormat]
        public string Phone { get; set; }

        [NullOrEmpty]
        [EmailValidator]
        [RemoveSpaces]
        public string Email { get; set; }

        [NullOrEmpty]
        public DateTime HireDate { get; set; }

        [NullOrEmpty]
        [RemoveSpaces]
        [QtdCaractersValidator(2, "Inserir apenas 2 caracteres.")]
        [UfValidator("Estado inválido")]
        public string State { get; set; }

        [NullOrEmpty]
        [RemoveSpaces]
        public string City { get; set; }

		[NullOrEmpty]
        [RemoveSpaces]
        public string Street { get; set; }

        [NullOrEmpty]
        [RemoveSpaces]
        public string Cep { get; set; }
       
        [NullOrEmpty]
        public int Number { get; set; }
        
        [NullOrEmpty]
        [RemoveSpaces]
        public string Neighborhood { get; set; }
     
        [NullOrEmpty]
        public StatusEmployee StatusEmployee { get; set; }
        
        [NullOrEmpty]
        public int PositionId { get; set; }
    }
}
