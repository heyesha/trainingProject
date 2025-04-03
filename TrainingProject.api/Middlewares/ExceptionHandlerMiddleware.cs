using System.Net;
using System.Net.Security;
using TrainingProject.Domain.Enum;
using TrainingProject.Domain.Result;
using ILogger = Serilog.ILogger;

namespace TrainingProject.Api.Middlewares;

/// <summary>
/// 
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        _logger.Error(exception, exception.Message);

        var errorMessage = exception.Message;
        var response = exception switch
        {
            UnauthorizedAccessException _ => new BaseResult() { ErrorMessage = exception.Message, ErrorCode = (int)HttpStatusCode.Unauthorized },
            _ => new BaseResult() { ErrorMessage = "Internal server error. Please retry later", ErrorCode = (int)HttpStatusCode.InternalServerError },
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)response.ErrorCode;
        await httpContext.Response.WriteAsJsonAsync(response);
    }
}
