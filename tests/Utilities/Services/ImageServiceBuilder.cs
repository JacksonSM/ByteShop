using ByteShop.Application.Services;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Exceptions;
using Moq;
using System;

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
            .ReturnsAsync(It.IsAny<string>());
        return this;
    }

    public ImageServiceBuilder SetupItsValid(bool valid)
    {
        if (valid)
        {
            _service.Setup(
                m => m.ItsValid(It.IsAny<string>(), It.IsAny<string>())).Returns("");
        }
        else
        {
            _service.Setup(
                m => m.ItsValid(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Algum erro!");
        }
        return this;
    }

    public ImageServiceBuilder SetupItsValidWithArrayParameter(bool valid)
    {
        if (valid)
        {
            _service.Setup(
                m => m.ItsValid(It.IsAny<string>(), It.IsAny<string>())).Returns("");
        }
        else
        {
            _service.Setup(
                m => m.ItsValid(It.IsAny<ImageBase64[]>()))
                .Returns("Algum erro!");
        }
        return this;
    }

    public IImageService Build()
    {
        return _service.Object;
    }
}
