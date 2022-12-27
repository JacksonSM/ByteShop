using ByteShop.Application.Services;
using Moq;

namespace Utilities.Services;
public class ImageServiceBuilder
{
    private static ImageServiceBuilder _instance;
    private readonly Mock<IImageService> _service;

    private ImageServiceBuilder()
    {
        if (_service is null)
        {
            _service = new Mock<IImageService>();
        }
    }
    public static ImageServiceBuilder Instance()
    {
        _instance = new ImageServiceBuilder();
        return _instance;
    }

    public ImageServiceBuilder SetupUpload()
    {
        _service.Setup(m => m.UploadBase64ImageAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync("http://anylink.com");

        return this;
    }

    public IImageService Build()
    {
        return _service.Object;
    }
    public Mock<IImageService> GetMock()
    {
        return _service;
    }
}
