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
        CreateMap<Domain.Entities.Product, ProductDTO>()
            .ForMember(x => x.SecondaryImageUrl,
            x => x.MapFrom(x => x.SecondaryImageUrl));

        CreateMap<Domain.Entities.Category, CategoryDTO>();
        CreateMap<Domain.Entities.Category, CategoryWithAssociationDTO>();
    }
}
