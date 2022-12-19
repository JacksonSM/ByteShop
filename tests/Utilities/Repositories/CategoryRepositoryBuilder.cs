using ByteShop.Domain.Interfaces.Repositories;
using Moq;

namespace Utilities.Repositories;
public class CategoryRepositoryBuilder
{
    private static CategoryRepositoryBuilder _instance;
    private readonly Mock<ICategoryRepository> _repository;

    private CategoryRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<ICategoryRepository>();
        }
    }
    public static CategoryRepositoryBuilder Instance()
    {
        _instance = new CategoryRepositoryBuilder();
        return _instance;
    }
    public CategoryRepositoryBuilder SetupExistById(int id)
    {
        if (id != 0)
            _repository.Setup(i => i.ExistsById(id)).ReturnsAsync(true);

        return this;
    }

    public ICategoryRepository Build()
    {
        return _repository.Object;
    }
}
