using System.Collections.Immutable;

namespace ByteShop.Domain.Entities.ProductAggregate;
public class ImagesUrl
{
    private string _mainImageUrl = string.Empty;
    private string _secondaryImageUrl = string.Empty;

    public string MainImageUrl { get => _mainImageUrl; }  
    public ImmutableList<string> SecondaryImages 
    { 
        get => _secondaryImageUrl.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToImmutableList(); 
    }

    public void AddSecondaryImage(string newImageUrl)
    {
        var urls = SecondaryImages.ToList();
        urls.Add(newImageUrl);
        _secondaryImageUrl = string.Join(" ", urls);
    }

    public void AddSecondaryImage(string[] imageUrls)
    {
        var urls = SecondaryImages.ToList();
        urls.AddRange(imageUrls);
        _secondaryImageUrl = string.Join(" ", urls);
    }

    public void RemoveSecondaryImage(string url)
    {
        var urls = SecondaryImages.ToList();
        urls.Remove(url);
        _secondaryImageUrl = string.Join(" ", urls);
    }

    public void SetMainImage(string imageUrl)
    {
        _mainImageUrl = imageUrl;
    }

    public int GetImagesTotal()
    {
        int total = 0;
        if (!string.IsNullOrEmpty(_mainImageUrl)) total++;
        total += SecondaryImages?.Count ?? 0;
        return total;
    }

    public List<string> GetAll()
    {
        var images = new List<string>{MainImageUrl};
        images.AddRange(SecondaryImages);
        return images;
    }
}