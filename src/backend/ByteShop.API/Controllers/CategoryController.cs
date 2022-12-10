using ByteShop.API.Tools;
using ByteShop.Application.DTOs;
using ByteShop.Application.UseCases.Commands;
using ByteShop.Application.UseCases.Commands.Category;
using ByteShop.Application.UseCases.Handlers.Category;
using ByteShop.Application.UseCases.Results;
using Microsoft.AspNetCore.Authorization;
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

    /// <summary>
    /// Atualiza categoria.
    /// </summary>
    /// <param name="id">Id da categoria</param>
    /// <remarks>
    /// Se o campo parentCategoryId for igual a 0, categoria pai não será atualizado
    /// </remarks>
    /// <response code="200">Retorna a categoria com as propriedades atualizadas.</response>
    /// <response code="400">Provavelmente as propriedades estão inválidos, Verifique a mensagem de erro.</response>
    /// <response code="404">Categoria não foi encontrado.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RequestResult<string>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RequestResult<string>))]
    public async Task<ActionResult> Update(
    int id,
    [FromBody] UpdateCategoryCommand command,
    [FromServices] UpdateCategoryHandler handler)
    {
        command.SetId(id);
        return new ParseRequestResult<CategoryDTO>().ParseToActionResult(await handler.Handle(command));
    }

    /// <summary>
    /// Deleta categoria por ID.
    /// </summary>
    /// <param name="id">Id da categoria</param>
    /// <response code="202">Categoria deletado com sucesso</response>
    /// <response code="404">Categoria não foi encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(RequestResult<object>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RequestResult<object>))]
    public async Task<ActionResult> Delete(
    int id,
    [FromServices] DeleteCategoryHandler handler)
    {
        return new ParseRequestResult<object>()
            .ParseToActionResult(await handler.Handle(new IdCommand { Id = id }));
    }
}
