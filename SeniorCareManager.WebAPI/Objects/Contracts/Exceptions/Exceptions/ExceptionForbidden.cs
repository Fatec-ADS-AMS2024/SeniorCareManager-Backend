using SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Base;

namespace SeniorCareManager.WebAPI.Objects.Contracts.Exceptions.Exceptions;

public class ExceptionForbidden : _ClientErrorException
{
    public ExceptionForbidden(string message) : base(403, message) { }
}
