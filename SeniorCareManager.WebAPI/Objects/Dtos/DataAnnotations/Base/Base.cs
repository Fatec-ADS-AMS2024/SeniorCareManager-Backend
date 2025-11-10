using System.Reflection;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

[AttributeUsage(AttributeTargets.Property)]
public abstract class BaseAnnotation : Attribute
{
    private PropertyInfo? _property;
    private object? _value;

    public string ErrorMessage { get; set; } = null!;

    protected object Value
    {
        get => _value ?? null!;
        set => _value = value;
    }

    protected string NameProperty => _property?.Name ?? string.Empty;

    public void Initialize(PropertyInfo property, object value)
    {
        _property = property;
        _value = value;
    }

    public abstract FieldError? Execute();

    protected FieldError ReturnError(string field, string? mensage = null)
    {
        if (ErrorMessage is not null)
            return new FieldError{Field = char.ToLower(field[0]) + field.Substring(1), Message = ErrorMessage};

        if (mensage is not null)
            return new FieldError{Field = char.ToLower(field[0]) + field.Substring(1), Message = mensage};

        return new FieldError{Field = char.ToLower(field[0]) + field.Substring(1), Message = "Erro padrão de anotação."};
    }
}
