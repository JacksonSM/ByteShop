using ByteShop.Application.Commands.Product;
using ByteShop.Application.DTOs;
using ByteShop.Application.Queries;
using ByteShop.Application.Reponses;
using ByteShop.Application.Services.Contracts;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ByteShop.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductAppService _productAppService;

    public ProductController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }


    /// <summary>
    /// Adiciona um produto na base de dados.
    /// </summary>
    /// <response code="201">Retorna o produto adicionado.</response>
    /// <response code="400">Provavelmente as propriedades estão inválido, Verifique a mensagem de erro.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ValidationResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    public async Task<ActionResult> Add([FromBody] AddProductCommand command)
    {
        var response = await _productAppService.Add(command);
        return response.IsValid ? Created(string.Empty, response) : BadRequest(response);
    }

    /// <summary>
    /// Atualiza produto.
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <remarks>
    /// Se o campo categoryId for igual a 0, a categoria do produto não será atualizado
    /// </remarks>
    /// <response code="200">Retorna o produto com as propriedades atualizadas.</response>
    /// <response code="400">Provavelmente as propriedades estão inválidos, Verifique a mensagem de erro.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValidationResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationResult))]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateProductCommand command)
    {
        command.SetId(id);
        var response = await _productAppService.Update(command);
        return response.IsValid ? Ok(response) : BadRequest(response);
    }

    /// <summary>
    /// Deleta produto por ID.
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <response code="202">Produto deletado com sucesso</response>
    /// <response code="404">Produto não foi encontrado.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var response = await _productAppService.Delete(new DeleteProductCommand(id));
        return response.IsValid ? Accepted() : NotFound();
    }

    /// <summary>
    /// Retorna produto por ID.
    /// </summary>
    /// <param name="id">Id do produto</param>
    /// <response code="200">Retorna o produto com o ID correspondente.</response>
    /// <response code="404">Produto não foi encontrado.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetById(int id)
    {
        var response = await _productAppService.GetById(id);
        return response is not null ? Ok(response) : NotFound();
    }

    /// <summary>
    /// Retorna todos os produtos com ou sem paginação.
    /// </summary>
    /// <response code="200">Retorna lista de produtos.</response>
    /// <response code="204">Não foi encontrado nenhum produto.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllProductsResponse))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetAll([FromQuery] GetAllProductsQuery query)
    {
        var response = await _productAppService.GetAll(query);
        
        if(!response.Content.Any())
            return NoContent();

        return Ok(response);
    }
}
