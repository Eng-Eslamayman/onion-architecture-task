using Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode status;
        string message;

        switch (exception)
        {
            case AppException.RequirementException:
                status = HttpStatusCode.Conflict;
                message = exception.Message;
                break;
            case AppException.BadRequestException:
                status = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
            case AppException.UnAuthorizedException:
                status = HttpStatusCode.Unauthorized;
                message = exception.Message;
                break;
            case AppException.NotFoundException:
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            default:
                status = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        context.Response.StatusCode = (int)status;

        var result = JsonSerializer.Serialize(new { description = message });
        return context.Response.WriteAsync(result);
    }
}
