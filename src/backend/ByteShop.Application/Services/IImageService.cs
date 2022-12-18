using ByteShop.Application.UseCases.Commands.Product;

namespace ByteShop.Application.Services;
public interface IImageService
{
    Task<string> UploadBase64ImageAsync(string base64Image, string extension);
    Task<bool> DeleteImageAsync(string imageUrl);

    /// <summary>
    /// Este metodo irar validar o tamanho da imagem e o tipo da extensão.
    /// </summary>
    /// <param name="imageBase64">imagem em base64</param>
    /// <param name="extension">a extensão da imagem</param>
    /// <returns>
    /// Retorna uma string vazia se a imagem estiver valido,
    /// caso contrario, retorna uma string especificando o erro.
    /// </returns>
    string ItsValid(string imageBase64, string extension);

    /// <summary>
    /// Este metodo irar validar o tamanho e o tipo da extensão de uma lista de imagens.
    /// </summary>
    /// <param name="imagesBase64">
    /// uma classe que conter valor base64 de uma imagem e
    /// outro campo com a extensão da imagem
    /// </param>
    /// <returns>
    /// Retorna uma string vazia se a imagem estiver valido,
    /// caso contrario, retorna uma string especificando o erro.
    /// </returns>
    string ItsValid(ImageBase64[] imagesBase64);
}
