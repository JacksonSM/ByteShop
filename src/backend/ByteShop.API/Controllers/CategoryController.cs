using ByteShop.Application.Commands.Category;
using ByteShop.Application.DTOs;
using ByteShop.Application.Services.Contracts;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ByteShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]

public class CategoryController : ControllerBase
{
    private readonly ICategoryAppService _categoryService;

    public CategoryController(ICategoryAppService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Adiciona uma categoria na base de dados.
    /// </summary>
    /// <response code="201">Retorna a categoria adicionado.</response>
    /// <response code="400">Provavelmente as propriedades estão inválido, Verifique a mensagem de erro.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    public async Task<ActionResult> Add([FromBody] AddCategoryCommand command)
    {
        var response = await _categoryService.Add(command);
        return response.IsValid ? Created(string.Empty, response) : BadRequest(response);
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
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCategoryCommand command)
    {
        command.SetId(id);
        var response = await _categoryService.Update(command);
        return response.IsValid ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Deleta categoria por ID.
    /// </summary>
    /// <param name="id">Id da categoria</param>
    /// <response code="202">Categoria deletado com sucesso</response>
    /// <response code="404">Categoria não foi encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete( int id)
    {
        var response = await _categoryService.Delete(new DeleteCategoryCommand(id));
        return response.IsValid ? Accepted() : BadRequest(response);
    }
    /// <summary>
    /// Retorna todos as categorias.
    /// </summary>
    /// <response code="200">Retorna lista de categorias.</response>
    /// <response code="204">Não foi encontrado nenhuma categoria.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO[]))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetAll()
    {
        var response = await _categoryService.GetAll();
        return response.Any() ? Ok(response) : NoContent();
    }

    /// <summary>
    /// Retorna categoria por ID.
    /// </summary>
    /// <param name="id">Id da categoria</param>
    /// <response code="200">Retorna a categoria com o ID correspondente.</response>
    /// <response code="404">Categoria não foi encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryDTO[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById(int id)
    {
        var response = await _categoryService.GetById(id);
        return response is not null ? Ok(response) : NotFound();
    }


}
