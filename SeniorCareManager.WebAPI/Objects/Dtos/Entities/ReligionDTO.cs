namespace SeniorCareManager.WebAPI.Objects.Dtos.Entities;

public class ReligionDTO
{

    public int Id { get; set; }
    public string Name { get; set; }

    public bool CheckName()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return false;
        return true;
    }
}

