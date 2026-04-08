using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace school_api.Middleware.ExceptionHandlers
{
    public class NotFoundExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception) =>
            exception is KeyNotFoundException;

        public ProblemDetails Handle(Exception exception, string traceId, bool includeDetails)
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.NotFound,
                Title = "Not found",
                Detail = includeDetails ? exception.Message : null,
                Extensions = { ["traceId"] = traceId }
            };
        }
    }
}
