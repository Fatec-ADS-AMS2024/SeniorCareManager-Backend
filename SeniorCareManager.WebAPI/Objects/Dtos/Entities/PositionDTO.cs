namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class PositionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static bool IsFilledString(params string[] parametros)
        {
            foreach (var parametro in parametros)
            {
                if (string.IsNullOrWhiteSpace(parametro))
                {
                    return false;
                }
            }
            return true;
        }
    }
 }
