using Azure.Storage.Blobs;
using ByteShop.Application.Services;
using ByteShop.Application.UseCases.Commands.Product;
using ByteShop.Exceptions;
using ByteShop.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace ByteShop.Infrastructure.Services;
public class ImageService : IImageService
{
    private const int MAXIMUM_IMAGE_SIZE_IN_BYTES = 350000;

    private readonly ImageContainerOptions _options;

    public ImageService(IOptions<ImageContainerOptions> options)
    {
        _options = options.Value;
    }

    public async Task<string> UploadBase64ImageAsync(string base64Image, string extension)
    {
        var fileName = Guid.NewGuid().ToString() + extension;

        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

        byte[] imageBytes = Convert.FromBase64String(data);

        var blobClient = new BlobClient(_options.ConnectionString, _options.NameContainer, fileName);

        using (var stream = new MemoryStream(imageBytes))
        {
            await blobClient.UploadAsync(stream);
        }

        return blobClient.Uri.AbsoluteUri;
    }


    public async Task<bool> DeleteImageAsync(string imageUrl)
    {
        var blobClient = new BlobClient(new Uri(imageUrl));
        var response = await blobClient.DeleteAsync();
        return response.IsError;
    }

    public string ItsValid(string imageBase64, string extension)
    {
        if (!ValidateExtension(extension))
            return ResourceErrorMessages.IMAGE_EXTENSION;
        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(imageBase64, "");

        byte[] imageBytes = Convert.FromBase64String(data);
        if (imageBytes.Length > MAXIMUM_IMAGE_SIZE_IN_BYTES)
            return  ResourceErrorMessages.MAX_IMAGE_SIZE;

        return string.Empty;
    }

    private static bool ValidateExtension(string extension) 
    {
        if (extension is null)
            return false;

        return extension.Equals(".jpeg") ||
               extension.Equals(".jpg") ||
               extension.Equals(".jpe") ||
               extension.Equals(".webp");
    }

    public string ItsValid(ImageBase64[] imagesBase64)
    {
        foreach (var image in imagesBase64)
        {
            var result = ItsValid(image.Base64, image.Extension);
            if (!string.IsNullOrEmpty(result))
                return result;
        }
        return string.Empty;
    }
}
