using ByteShop.Application.Reponses;
using ByteShop.Infra.CrossCutting.Bus;

namespace ByteShop.Application.Queries;
public class GetAllProductsQuery : Query<GetAllProductsResponse>
{
#nullable enable
    public string? sku { get; set; }
    public string? name { get; set; }
    public string? brand { get; set; }
    public string? category { get; set; }
    public int? actualPage { get; set; }
    public int? itemsPerPage { get; set; }
}
