using Microsoft.AspNetCore.Mvc;

namespace school_api.Middleware.ExceptionHandlers
{
    public interface IExceptionHandler
    {
        bool CanHandle(Exception exception);
        ProblemDetails Handle(Exception exception, string traceId, bool includeDetails);
    }
}
