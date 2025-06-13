public class ReligionDTO
{
    private string _name;
    private int? _id;

    public int? Id
    {
        get => _id;
        set
        {
            if (value == null)
                throw new ArgumentException("O ID não pode ser nulo.");
            _id = value.Value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("O nome da religião é obrigatório.");
            _name = value.Trim();
        }
    }
}
