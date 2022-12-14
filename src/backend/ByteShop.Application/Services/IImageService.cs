namespace ByteShop.Application.Services;
public interface IImageRepository
{
    Task<string> UploadBase64ImageAsync(string base64Image, string extension);
    Task<bool> DeleteImageAsync(string imageUrl);
    (bool, string) ItsValid(string imageBase64, string extension);
    (bool, string) ItsValid(Tuple<string, string>[] imagesBase64);
}
