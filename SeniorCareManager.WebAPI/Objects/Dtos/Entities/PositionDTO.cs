namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class PositionDTO
    {
        public int Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value.Trim();
        }
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
