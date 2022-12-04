namespace ByteShop.Domain.Interfaces.Repositories;
public interface ICategoryRepository
{
    Task<bool> ExistsById(int id);
}
