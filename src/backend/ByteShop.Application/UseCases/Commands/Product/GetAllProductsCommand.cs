namespace ByteShop.Application.UseCases.Commands.Product;
public class GetAllProductsCommand : ICommand
{
    #nullable enable
    public string? sku { get; set; }
    public string? name { get; set; }
    public string? brand { get; set; }
    public string? category { get; set; }
    public int? actualPage { get; set; }
    public int? itemsPerPage { get; set; }
}
