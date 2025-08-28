using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Format;

namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class PositionDTO
    {
        public int Id { get; set; }

        [NullOrEmpty(ErrorMessage = "O campo não pode ser nulo ou vazio.")]
        [ValidateQtdCaracters(1, 50,ErrorMessage = "O campo deve ter entre 1 e 50 caracteres.")]
        public string Name { get; set; }

    }
 }
