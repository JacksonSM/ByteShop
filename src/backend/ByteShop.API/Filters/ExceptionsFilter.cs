using ByteShop.API.Tools;
using ByteShop.Application.UseCases.Results;
using ByteShop.Exceptions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ByteShop.API.Filters;

public class ExceptionsFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ByteShopException:
                HandleByteShopException(context);
                break;
            default:
                HandleUnknownError(context);
                break;
        }

    }

    private static void HandleByteShopException(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorsException)
        {
            HandleValidationErrors(context);
        }
        else if(context.Exception is DomainExecption)
        {
            HandleDomainExecption(context);
        }
        else
        {
            context.Result = new ParseRequestResult<string>()
                .ParseToActionResult(new RequestResult<string>()
                .BadRequest("Operação invalida", context.Exception.Message));
        }
    }

    private static void HandleDomainExecption(ExceptionContext context)
    {
        context.Result = new ParseRequestResult<string>()
            .ParseToActionResult(new RequestResult<string>()
            .BadRequest("Operação invalida", context.Exception.Message));
    }

    private static void HandleUnknownError(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new { ErrorMessage = "Erro desconhecido" });
    }

    private static void HandleValidationErrors(ExceptionContext context)
    {
        var errors = context.Exception as ValidationErrorsException;
        context.Result = new ParseRequestResult<List<string>>()
            .ParseToActionResult(new RequestResult<List<string>>()
            .BadRequest("Erros de validaçãoes", errors.ErrorMessages));
    }
}
