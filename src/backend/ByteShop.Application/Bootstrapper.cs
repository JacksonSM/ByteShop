using ByteShop.Application.Tools;
using ByteShop.Application.UseCases.Handlers.Product;
using Microsoft.Extensions.DependencyInjection;

namespace ByteShop.Application;
public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddMapping(services);
        AddHandlers(services);
    }

    private static void AddMapping(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperConfig));
    }

    private static void AddHandlers(IServiceCollection services)
    {
        services.AddScoped<AddProductHandler>();
    }
}
