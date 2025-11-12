using System.Reflection;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;

namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;

public static class Execute
{
    public static void Executar(object obj)
    {
        var errors = new List<FieldError>();
        var properties = obj.GetType().GetProperties();
        if (!properties.Any())
            return;

        foreach (var property in properties)
        {
            var executions = property.GetCustomAttributes<BaseAnnotation>(true);


            if (!executions.Any())
                continue;


            foreach (var action in executions)
            {
                action.Initialize(property, obj);
                var erro = action.Execute();
                if (erro != null)
                    errors.Add(erro);
            }

        }
        if (errors.Count() > 0)
            throw new ExceptionBadRequest("Erros de validação", errors);
    }
}
