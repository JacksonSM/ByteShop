using ByteShop.API.Tools;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Handlers.Product;
using ByteShop.Application.UseCases.Results;
using Microsoft.AspNetCore.Mvc;

namespace ByteShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    /// <summary>
    /// Adiciona um produto na base de dados.
    /// </summary>
    /// <response code="201">Retorna o produto adicionado.</response>
    /// <response code="400">Provavelmente as propriedades estão inválido, Verifique a mensagem de erro.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestResult<ProductDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RequestResult<string[]>))]
    public async Task<ActionResult> Add(
    [FromBody] AddProductCommand command,
    [FromServices] AddProductHandler handler)
    {
        return new ParseRequestResult<ProductDTO>().ParseToActionResult(await handler.Handle(command));
    }
}
