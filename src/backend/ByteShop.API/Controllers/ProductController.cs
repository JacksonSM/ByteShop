using ByteShop.API.Tools;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
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

    /// <summary>
    /// Retorna produto por ID.
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <response code="200">Retorna o produto com o ID correspondente.</response>
    /// <response code="404">Produto não foi encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequestResult<ProductDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> GetById(
    int id,
    [FromServices] GetProductByIdHandler handler)
    {
        return new ParseRequestResult<ProductDTO>().ParseToActionResult(await handler.Handle(new GetByIdCommand { Id = id }));
    }

    /// <summary>
    /// Retorna todos os produtos com ou sem paginação.
    /// </summary>
    /// <remarks>As informaçoes da paginação estão no header com a chave: "X-Pagination"</remarks>
    /// <response code="200">Retorna lista de videos.</response>
    /// <response code="204">Não foi encontrado nenhum video.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequestResult<ProductDTO[]>))]
    public async Task<ActionResult> GetAll(
    [FromQuery] GetAllProductsCommand command,
    [FromServices] GetAllProductsHandler handler)
    {
        return new ParseRequestResult<IEnumerable<ProductDTO>>().ParseToActionResult(await handler.Handle(command), Response);
    }

}
