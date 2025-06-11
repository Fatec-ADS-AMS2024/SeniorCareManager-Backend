namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities
{
    public class AllergyDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public bool CheckName()
        {
            if (string.IsNullOrWhiteSpace(Name))
                return false;
            return true;
        }

    }
}
