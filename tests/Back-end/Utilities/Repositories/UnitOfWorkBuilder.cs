using ByteShop.Domain.Interfaces.Repositories;
using Moq;

namespace Utilities.Repositories;
public class UnitOfWorkBuilder
{
    private static UnitOfWorkBuilder? _instance;
    private readonly Mock<IUnitOfWork> _uow;

    private UnitOfWorkBuilder()
    {
        if (_uow is null)
        {
            _uow = new Mock<IUnitOfWork>();
        }
    }

    public static UnitOfWorkBuilder Instance()
    {
        _instance = new UnitOfWorkBuilder();
        return _instance;
    }

    public IUnitOfWork Build()
    {
        return _uow.Object;
    }
}
