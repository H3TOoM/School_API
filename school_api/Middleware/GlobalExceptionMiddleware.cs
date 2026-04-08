using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using school_api.Middleware.ExceptionHandlers;

namespace school_api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IEnumerable<IExceptionHandler> _exceptionHandlers;

        public GlobalExceptionMiddleware(
            RequestDelegate next, 
            ILogger<GlobalExceptionMiddleware> logger,
            IEnumerable<IExceptionHandler> exceptionHandlers)
        {
            _next = next;
            _logger = logger;
            _exceptionHandlers = exceptionHandlers;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var traceId = context.TraceIdentifier;
            var includeDetails = context.RequestServices
                .GetRequiredService<IHostEnvironment>()
                .IsDevelopment();

            var handler = _exceptionHandlers.FirstOrDefault(h => h.CanHandle(exception));
            var problem = handler?.Handle(exception, traceId, includeDetails) 
                ?? new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Internal server error",
                    Detail = includeDetails ? exception.Message : "An error occurred while processing your request.",
                    Extensions = { ["traceId"] = traceId }
                };

            context.Response.StatusCode = problem.Status ?? (int)HttpStatusCode.InternalServerError;
            problem.Instance = context.Request.Path;

            if (includeDetails && exception.InnerException?.Message is { Length: > 0 } inner)
                problem.Extensions["inner"] = inner;

            var jsonResponse = JsonSerializer.Serialize(problem, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
