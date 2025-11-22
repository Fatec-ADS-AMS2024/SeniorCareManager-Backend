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
        get => GetValue();
        set => SetValue(value);
    }

    private object GetValue()
    {
        return _property?.GetValue(_value) ?? string.Empty;
    }

    private void SetValue(object newValue)
    {
        _property?.SetValue(_value, newValue);
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
