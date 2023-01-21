using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ByteShop.API.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionsFilter> _logger;

    public ExceptionsFilter(ILogger<ExceptionsFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        HandleUnknownError(context);
    }

    private void HandleUnknownError(ExceptionContext context)
    {
        _logger.LogInformation("An unknown error has occurred");
        _logger.LogDebug("Error message", context.Exception);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new { ErrorMessage = "Erro desconhecido" });
    }
}
