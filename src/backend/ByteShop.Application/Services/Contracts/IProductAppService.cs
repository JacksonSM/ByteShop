using ByteShop.Application.Commands.Product;
using ByteShop.Application.DTOs;
using ByteShop.Application.Queries;
using ByteShop.Application.Reponses;
using FluentValidation.Results;

namespace ByteShop.Application.Services.Contracts;
public interface IProductAppService
{
    Task<ValidationResult> Add(AddProductCommand command);
    Task<ValidationResult> Update(UpdateProductCommand command);
    Task<ValidationResult> Delete(DeleteProductCommand command);

    Task<ProductDTO> GetById(int id);
    Task<GetAllProductsResponse> GetAll(GetAllProductsQuery query);
}
