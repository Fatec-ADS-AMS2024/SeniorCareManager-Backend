using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class PositionDTO
    {
        public int Id { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [RemoveSpaces]
        public string Name { get; set; }

    }
 }
