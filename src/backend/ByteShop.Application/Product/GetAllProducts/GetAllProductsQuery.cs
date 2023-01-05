using MediatR;

namespace ByteShop.Application.Product.GetAllProducts;
public class GetAllProductsQuery : IRequest<GetAllProductsResponse>
{
#nullable enable
    public string? sku { get; set; }
    public string? name { get; set; }
    public string? brand { get; set; }
    public string? category { get; set; }
    public int? actualPage { get; set; }
    public int? itemsPerPage { get; set; }
}
