using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace school_api.Middleware.ExceptionHandlers
{
    public class BadRequestExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception) =>
            exception is ArgumentNullException or ArgumentException;

        public ProblemDetails Handle(Exception exception, string traceId, bool includeDetails)
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Bad request",
                Detail = includeDetails ? exception.Message : null,
                Extensions = { ["traceId"] = traceId }
            };
        }
    }
}
