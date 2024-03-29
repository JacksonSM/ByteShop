﻿using AutoMapper;
using ByteShop.Application.DTOs;

namespace ByteShop.Application.Tools;
public class MapperConfig : Profile
{
    public MapperConfig()
    {
        EntityForDTO();
    }

    private void EntityForDTO()
    {
        CreateMap<Domain.Entities.ProductAggregate.Product, ProductDTO>()
            .ForMember(x => x.SecondaryImageUrl,
                x => x.MapFrom(x => x.ImagesUrl.SecondaryImages))
            .ForMember(x => x.MainImageUrl,
                x => x.MapFrom(x => x.ImagesUrl.MainImageUrl));

        CreateMap<Domain.Entities.Category, CategoryDTO>();
        CreateMap<Domain.Entities.Category, CategoryWithAssociationDTO>();
    }
}
