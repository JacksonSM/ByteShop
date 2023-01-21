using ByteShop.Domain.Entities.ProductAggregate;
using ByteShop.Domain.Interfaces.Repositories;
using Moq;

namespace Utilities.Repositories;
public class ProductRepositoryBuilder
{
    private static ProductRepositoryBuilder? _instance;
    private readonly Mock<IProductRepository> _repository;

    private ProductRepositoryBuilder()
    {
        if (_repository is null)
        {
            _repository = new Mock<IProductRepository>();
        }
    }
    public static ProductRepositoryBuilder Instance()
    {
        _instance = new ProductRepositoryBuilder();
        return _instance;
    }

    public ProductRepositoryBuilder SetupGetById(Product product)
    {
        _repository.Setup(m => m.GetByIdAsync(product.Id)).ReturnsAsync(product);
        return this;
    }

    public IProductRepository Build()
    {
        return _repository.Object;
    }
    public Mock<IProductRepository> GetMock()
    {
        return _repository;
    }
}
