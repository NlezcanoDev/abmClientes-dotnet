using System.Net;
using Amb.Clientes.Domain.Models;
using NuGet.Protocol;

namespace Amb.Clientes.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = HandleStatusCode(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new BaseResponseModel
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message
        };

        return context.Response.WriteAsync(response.ToJson() ?? string.Empty);
    }

    private static HttpStatusCode HandleStatusCode(Exception exception)
    {
        HttpStatusCode statusCode;
        switch (exception)
        {
            case ArgumentNullException
                or ArgumentException
                or NullReferenceException
                or IndexOutOfRangeException
                or InvalidOperationException:
                statusCode = HttpStatusCode.BadRequest;
                break;
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                break;
            case FileNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                break;
            default:
                statusCode = HttpStatusCode.InternalServerError;
                break;
        }

        return statusCode;
    }
}