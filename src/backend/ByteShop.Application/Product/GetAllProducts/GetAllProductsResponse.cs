using ByteShop.Application.DTOs;
using ByteShop.Application.Reponses;

namespace ByteShop.Application.Product.GetAllProducts;
public class GetAllProductsResponse 
{
    public ProductDTO[] Content { get; set; }
    public Pagination Pagination { get; set; }

    public GetAllProductsResponse(ProductDTO[] productDTOs,
        Pagination pagination) : this(productDTOs)
    {
        Pagination = pagination;
    }

    public GetAllProductsResponse(ProductDTO[] productDTOs)
    {
        Content = productDTOs;
    }
}
