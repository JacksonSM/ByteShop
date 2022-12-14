using Azure.Storage.Blobs;
using ByteShop.Application.Services;
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

    public (bool, string) ItsValid(string imageBase64, string extension)
    {
        if (!ValidateExtension(extension))
            return (false, ResourceErrorMessages.IMAGE_EXTENSION);

        byte[] imageBytes = Convert.FromBase64String(imageBase64);
        if (imageBytes.Length > MAXIMUM_IMAGE_SIZE_IN_BYTES)
            return (false, ResourceErrorMessages.MAX_IMAGE_SIZE);

        return (true, string.Empty);
    }

    private bool ValidateExtension(string extension) 
    {
        return extension.Equals(".jpeg") ||
               extension.Equals(".jpg") ||
               extension.Equals(".jpe") ||
               extension.Equals(".webp");
    }

    public (bool, string) ItsValid(Tuple<string, string>[] imagesBase64)
    {
        foreach (var image in imagesBase64)
        {
            var isValid = ItsValid(image.Item1, image.Item2);
            return (isValid.Item1, isValid.Item2);
        }
        return (true, string.Empty);
    }
}
