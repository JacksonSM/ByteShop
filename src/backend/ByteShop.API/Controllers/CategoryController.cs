using ByteShop.API.Tools;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Commands.Category;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Application.UseCases.Handlers.Category;
using ByteShop.Application.UseCases.Handlers.Product;
using ByteShop.Application.UseCases.Results;
using Microsoft.AspNetCore.Mvc;

namespace ByteShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    /// <summary>
    /// Adiciona uma categoria na base de dados.
    /// </summary>
    /// <response code="201">Retorna a categoria adicionado.</response>
    /// <response code="400">Provavelmente as propriedades estão inválido, Verifique a mensagem de erro.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RequestResult<CategoryDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RequestResult<string[]>))]
    public async Task<ActionResult> Add(
    [FromBody] AddCategoryCommand command,
    [FromServices] AddCategoryHandler handler)
    {
        return new ParseRequestResult<CategoryDTO>().ParseToActionResult(await handler.Handle(command));
    }
    /// <summary>
    /// Retorna todos as categorias.
    /// </summary>
    /// <response code="200">Retorna lista de categorias.</response>
    /// <response code="204">Não foi encontrado nenhuma categoria.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RequestResult<CategoryWithAssociationDTO[]>))]
    public async Task<ActionResult> GetAll(
    [FromServices] GetAllCategoryHandler handler)
    {
        return new ParseRequestResult<CategoryWithAssociationDTO[]>()
            .ParseToActionResult(await handler.Handle(new NoParametersCommand()), Response);
    }
}
