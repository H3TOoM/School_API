using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace school_api.Middleware.ExceptionHandlers
{
    public class ConflictExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception) =>
            exception is InvalidOperationException;

        public ProblemDetails Handle(Exception exception, string traceId, bool includeDetails)
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.Conflict,
                Title = "Conflict",
                Detail = includeDetails ? exception.Message : null,
                Extensions = { ["traceId"] = traceId }
            };
        }
    }
}
