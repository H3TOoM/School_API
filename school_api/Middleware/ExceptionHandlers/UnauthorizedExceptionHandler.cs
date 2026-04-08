using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace school_api.Middleware.ExceptionHandlers
{
    public class UnauthorizedExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception) =>
            exception is UnauthorizedAccessException;

        public ProblemDetails Handle(Exception exception, string traceId, bool includeDetails)
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Title = "Unauthorized",
                Detail = includeDetails ? exception.Message : null,
                Extensions = { ["traceId"] = traceId }
            };
        }
    }
}
