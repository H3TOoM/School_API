using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace school_api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
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

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var traceId = context.TraceIdentifier;

            var includeDetails = context.RequestServices
                .GetRequiredService<IHostEnvironment>()
                .IsDevelopment();

            ProblemDetails problem;

            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    problem = new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Title = "Bad request",
                        Detail = includeDetails ? exception.Message : null,
                        Instance = context.Request.Path
                    };
                    break;
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    problem = new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Title = "Unauthorized",
                        Detail = includeDetails ? exception.Message : null,
                        Instance = context.Request.Path
                    };
                    break;
                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    problem = new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Title = "Not found",
                        Detail = includeDetails ? exception.Message : null,
                        Instance = context.Request.Path
                    };
                    break;
                case InvalidOperationException:
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    problem = new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Title = "Conflict",
                        Detail = includeDetails ? exception.Message : null,
                        Instance = context.Request.Path
                    };
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    problem = new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Title = "Internal server error",
                        Detail = includeDetails ? exception.Message : "An error occurred while processing your request.",
                        Instance = context.Request.Path
                    };
                    break;
            }

            problem.Extensions["traceId"] = traceId;
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
