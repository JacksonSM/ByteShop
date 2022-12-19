using AutoMapper;
using ByteShop.Application.Tools;

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
