using ByteShop.API.Tools;
using ByteShop.Application.UseCases.Commands.User;
using ByteShop.Application.UseCases.Handlers.User;
using ByteShop.Application.UseCases.Results;
using Microsoft.AspNetCore.Mvc;

namespace ByteShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    /// <summary>
    /// Registra um usuario no sistema.
    /// </summary>
    /// <response code="201">Retorna a categoria adicionado.</response>
    /// <response code="400">Provavelmente os campos estão inválidos, Verifique a mensagem de erro.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RequestResult<string[]>))]
    public async Task<ActionResult> Add(
    [FromBody] RegisterCustomerCommand command,
    [FromServices] RegisterCustomerHandler handler)
    {
        return new ParseRequestResult<object>().ParseToActionResult(await handler.Handle(command));
    }
}
