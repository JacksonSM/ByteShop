using ByteShop.API.Tools;
using ByteShop.Application.UseCases.Results;
using ByteShop.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace ByteShop.API.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ByteShopException)
        {
            HandleByteShopException(context);
        }
        else
        {
            HandleUnknownError(context);
        }
    }

    private static void HandleUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new { ErrorMessage = "Erro desconhecido" });
    }

    private static void HandleByteShopException(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorsException)
        {
            HandleValidationErrors(context);
        }
    }

    private static void HandleValidationErrors(ExceptionContext context)
    {
        var errors = context.Exception as ValidationErrorsException;
        context.Result = new ParseRequestResult<List<string>>()
            .ParseToActionResult(new RequestResult<List<string>>()
            .BadRequest("Erros de validaçãoes", errors.ErrorMessages));
    }
}
