namespace ByteShop.Application.UseCases.Commands.Product;
public class GetAllProductsCommand : ICommand
{
    public string? sku { get; set; }
    public string? name { get; set; }
    public string? brand { get; set; }
    public string? category { get; set; }
    public int? ActualPage { get; set; }
    public int? ItemsPerPage { get; set; }
}
