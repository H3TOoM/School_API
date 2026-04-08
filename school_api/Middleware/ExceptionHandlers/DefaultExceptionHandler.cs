using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace school_api.Middleware.ExceptionHandlers
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public bool CanHandle(Exception exception) => true;

        public ProblemDetails Handle(Exception exception, string traceId, bool includeDetails)
        {
            return new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Internal server error",
                Detail = includeDetails ? exception.Message : "An error occurred while processing your request.",
                Extensions = { ["traceId"] = traceId }
            };
        }
    }
}
