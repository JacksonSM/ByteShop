using ByteShop.Application.Commands.User;
using ByteShop.Application.Services.Contracts;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ByteShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserAppService _userAppService;

    public AuthController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    /// <summary>
    /// Registra um usuario no sistema.
    /// </summary>
    /// <response code="201">Retorna a categoria adicionado.</response>
    /// <response code="400">Provavelmente os campos estão inválidos, Verifique a mensagem de erro.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    public async Task<ActionResult> Add([FromBody] RegisterCustomerCommand command)
    {
        var response = await _userAppService.RegisterCustomer(command);
        return response.IsValid ? Created(string.Empty, response) : BadRequest(response);
    }
}
