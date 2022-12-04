namespace ByteShop.Domain.Interfaces.Repositories;
public interface IUnitOfWork
{
    void Commit();
    Task CommitAsync();
}
