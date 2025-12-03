using System.Text.Json;
using WebAPI.Exceptions.Common;
using WebAPI.ResponseModels;

namespace WebAPI.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        logger.LogDebug("ExceptionHandlingMiddleware invoked.");
        try
        {
            await next(context);
        }
        catch (BaseException ex)
        {
            if (context.Response.HasStarted)
            {
                logger.LogWarning("Response has already started, cannot write error response.");
                return;
            }
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            if (context.Response.HasStarted)
            {
                logger.LogWarning("Response has already started, cannot write error response.");
                return;
            }
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        if (context.Response.HasStarted)
            return Task.CompletedTask;
        var statusCode = (ex as BaseException)?.StatusCode ?? 500;

        var response = Response<string>.Fail($"{ex.Message}", statusCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}

