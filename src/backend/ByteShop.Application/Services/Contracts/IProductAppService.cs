using ByteShop.Application.DTOs;
using ByteShop.Application.Product.AddProduct;
using ByteShop.Application.Product.GetAllProducts;
using ByteShop.Application.Product.RemoveProduct;
using ByteShop.Application.Product.UpdateProduct;
using FluentValidation.Results;

namespace ByteShop.Application.Services.Contracts;
public interface IProductAppService
{
    Task<AddProductResponse> Add(AddProductCommand command);
    Task<ValidationResult> Update(UpdateProductCommand command);
    Task<ValidationResult> Delete(DeleteProductCommand command);

    Task<ProductDTO> GetById(int id);
    Task<GetAllProductsResponse> GetAll(GetAllProductsQuery query);
}
