using AutoMapper;
using ByteShop.Application.Services;

namespace Utilities.Mapper;
public class MapperBuilder
{
    public static IMapper Instance()
    {

        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        return mockMapper.CreateMapper();
    }
}
