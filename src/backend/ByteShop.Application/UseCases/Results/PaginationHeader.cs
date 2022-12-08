using System.Text.Json.Serialization;

namespace ByteShop.Application.UseCases.Results;
public class PaginationHeader
{
    [JsonIgnore]
    public string Key { get; private set; }
    public int ActualPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int ItemsTotal { get; set; }
    public int TotalPage { get; set; }

    public PaginationHeader(int actualPage, int itemsPerPage, int itemsTotal)
    {
        ActualPage = actualPage;
        ItemsPerPage = itemsPerPage;
        ItemsTotal = itemsTotal;
        TotalPage = (ItemsTotal / ItemsPerPage);
        Key = "X-Pagination";
    }
}
