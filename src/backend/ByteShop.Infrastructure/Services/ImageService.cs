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

    public async Task DeleteImageAsync(string[] removeSecondaryImageUrl)
    {
        foreach (var url in removeSecondaryImageUrl)
        {
            await DeleteImageAsync(url);
        }
    }

    public async Task<bool> DeleteImageAsync(string imageUrl)
    {
        var blobClient = new BlobClient(new Uri(imageUrl));
        var response = await blobClient.DeleteAsync();
        return response.IsError;
    }
}
