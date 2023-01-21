namespace ByteShop.Application.Reponses;
public class Pagination
{
    public int ActualPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int ItemsTotal { get; set; }
    public int TotalPage { get; set; }

    public Pagination(int actualPage, int itemsPerPage, int itemsTotal)
    {
        ActualPage = actualPage;
        ItemsPerPage = itemsPerPage;
        ItemsTotal = itemsTotal;
        TotalPage = ItemsTotal / ItemsPerPage;
    }
}
