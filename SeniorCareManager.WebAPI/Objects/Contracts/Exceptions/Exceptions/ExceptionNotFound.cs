using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Base;

namespace SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionNotFound : _ClientErrorException
{
    public ExceptionNotFound(string message) : base(404, message) { }
}
