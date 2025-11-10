using System;
using System.Reflection;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;
using SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
using SeniorCareManager.WebAPI.Services.Utils;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Valid;

public class EnumValidator : BaseAnnotation
{
    private readonly Type _enumType;

    public EnumValidator(Type enumType)
    {
        if (enumType == null)
        {
            throw new ExceptionBadRequest("O tipo fornecido não pode ser nulo.");
        }

        if (!enumType.IsEnum)
        {
            throw new ExceptionBadRequest($"O tipo '{enumType.Name}' deve ser um Enum.");
        }

        _enumType = enumType;
    }

    public override FieldError? Execute()
    {
        Console.WriteLine($"=== DEBUG EnumValidator ===");
        Console.WriteLine($"Expected Enum Type: {_enumType.Name}");
        Console.WriteLine($"Actual Value Type: {Value?.GetType().Name}");
        Console.WriteLine($"Value: {Value}");
        Console.WriteLine($"Property Name: {NameProperty}");
        Console.WriteLine($"Value is Enum: {Value?.GetType().IsEnum}");

        // SE O VALUE É O OBJETO DTO COMPLETO, PRECISAMOS EXTRAIR O VALOR DA PROPRIEDADE
        object propertyValue = Value;

        if (Value != null && !Value.GetType().IsEnum && !Value.GetType().IsPrimitive && Value.GetType() != typeof(string))
        {
            // Value é o objeto DTO, precisamos extrair o valor da propriedade
            var propertyInfo = Value.GetType().GetProperty(NameProperty, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                propertyValue = propertyInfo.GetValue(Value);
                Console.WriteLine($"Extracted Property Value: {propertyValue} (Type: {propertyValue?.GetType().Name})");
            }
        }

        if (propertyValue.IsNull())
            return null;

        bool isValid = false;
        string attemptedValue = propertyValue.ToString();

        try
        {
            // SE O VALOR JÁ É DO TIPO ENUM (AllergyType)
            if (propertyValue.GetType() == _enumType || propertyValue.GetType().IsEnum)
            {
                // Valor já é do tipo enum correto, apenas verifica se é definido
                isValid = Enum.IsDefined(_enumType, propertyValue);
                Console.WriteLine($"Is defined as enum: {isValid}");
            }
            // SE É UM INT (valor numérico)
            else if (propertyValue is int intValue)
            {
                isValid = Enum.IsDefined(_enumType, intValue);
                Console.WriteLine($"Is defined as int: {isValid}");
            }
            else if (propertyValue is string stringValue)
            {
                // Tenta converter como string primeiro (para nomes do enum)
                if (Enum.TryParse(_enumType, stringValue, true, out _))
                {
                    isValid = true;
                    Console.WriteLine($"Is defined as string: {isValid}");
                }
                // Se não conseguiu, tenta como número
                else if (int.TryParse(stringValue, out int numericValue))
                {
                    isValid = Enum.IsDefined(_enumType, numericValue);
                    Console.WriteLine($"Is defined as parsed int: {isValid}");
                }
            }
            else
            {
                // Para outros tipos
                isValid = Enum.IsDefined(_enumType, propertyValue);
                Console.WriteLine($"Is defined as other type: {isValid}");
            }

            if (!isValid)
            {
                var validValues = Enum.GetValues(_enumType);
                var validValuesString = string.Join(", ",
                    validValues.Cast<object>()
                              .Select(v => $"{(int)v} ({v})"));

                // Use a ErrorMessage customizada se existir
                var errorMessage = !string.IsNullOrEmpty(ErrorMessage)
                    ? ErrorMessage
                    : $"O valor '{attemptedValue}' não é válido para {_enumType.Name}. Valores válidos: {validValuesString}";

                Console.WriteLine($"Validation FAILED: {errorMessage}");
                return ReturnError(NameProperty, errorMessage);
            }

            Console.WriteLine($"Validation SUCCESS");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validation ERROR: {ex.Message}");
            return ReturnError(NameProperty, $"Erro ao validar enum: {ex.Message}");
        }

        return null;
    }
}
