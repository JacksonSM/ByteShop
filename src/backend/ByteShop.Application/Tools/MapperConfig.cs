using AutoMapper;
using ByteShop.Application.DTOs;
using ByteShop.Domain.Entities;

namespace ByteShop.Application.Tools;
public class MapperConfig : Profile
{
    public MapperConfig()
    {
        EntityForDTO();
    }

    private void EntityForDTO()
    {
        CreateMap<Product, ProductDTO>()
            .ForMember(x => x.SecondaryImageUrl,
            x => x.MapFrom(x => x.GetSecondaryImageUrl()));

        CreateMap<Category, CategoryDTO>();
        CreateMap<Category, CategoryWithAssociationDTO>();
    }
}
