using ByteShop.Application.DTOs;
using ByteShop.Infra.CrossCutting.Bus;

namespace ByteShop.Application.Reponses;
public class GetAllProductsResponse : QueryResponse
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
