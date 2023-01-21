using Azure.Storage.Blobs;
using ByteShop.Domain.Interfaces.Services;
using ByteShop.Infrastructure.Options;
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
        var blobName = Path.GetFileName(imageUrl);
        var blobClient = new BlobClient(_options.ConnectionString, _options.NameContainer, blobName);
        var response = await blobClient.DeleteIfExistsAsync();
        return response.Value;
    }
}
