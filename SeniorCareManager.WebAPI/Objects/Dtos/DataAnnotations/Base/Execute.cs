    using System.Reflection;

    namespace SeniorCareManager.WebAPI.Objects.Dtos.DataAnnotations.Base;
    public static class Execute
    {
        public static void Executar(object obj)
        {
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
                    action.Execute();
                }

            }
        }
    }