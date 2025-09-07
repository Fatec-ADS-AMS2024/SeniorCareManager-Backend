using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Base;

namespace SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionUnauthorized : _ClientErrorException
{
    public ExceptionUnauthorized(string message) : base(401, message) { }
}
