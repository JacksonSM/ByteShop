using ByteShop.Infra.CrossCutting.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace ByteShop.Infra.CrossCutting;
public static class Bootstrapper
{
    public static void AddMediatorServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, InMemoryBus>();
    }
}