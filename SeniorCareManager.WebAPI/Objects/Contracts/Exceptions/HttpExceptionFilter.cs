using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Base;

namespace SeniorCareManager.WebAPI.Objects.Contracts.Exceptions;

public class HttpExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        Response<object> response;

        if (context.Exception is ApiException exception)
        {
            response = new Response<object>(status: exception.StatusCode, message: exception.Message);

            if (exception is _ValidationException validation && validation.Errors != null && validation.Errors.Count() > 0)
            {
                response = new Response<object>(status: exception.StatusCode, message: exception.Message, errors: validation.Errors);
            }
        }
        else
        {
            var errorDetails = new
            {
                ERRO_REAL = context.Exception.Message,
                ERRO_INTERNO = context.Exception.InnerException?.Message, // O '?' evita erro se InnerException for nulo
                STACK_TRACE = context.Exception.StackTrace
            };

            // 2. Passe esse objeto para o parâmetro 'data' do seu Response
            response = new Response<object>(
                status: 500,
                message: "Ocorreu um erro inesperado no servidor.",
                data: errorDetails // <-- Aqui está a correção
            );

            // Ex: _logger.LogError(context.Exception, "An unhandled exception has occurred.");
        }

        context.Result = new ObjectResult(response)
        {
            StatusCode = response.Status
        };

        context.ExceptionHandled = true;

    }
}