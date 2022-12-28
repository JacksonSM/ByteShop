using ByteShop.Application.Services;
using ByteShop.Domain.Account;
using ByteShop.Domain.Interfaces.Repositories;
using ByteShop.Infrastructure.Contexts;
using ByteShop.Infrastructure.Identity;
using ByteShop.Infrastructure.Options;
using ByteShop.Infrastructure.Repositories;
using ByteShop.Infrastructure.Services;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteShop.Infrastructure;
public static class Bootstrapper
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddServices(services);

        services.Configure<ImageContainerOptions>(
            options => configuration.GetSection(ImageContainerOptions.KEY).Bind(options));

    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BusinessDb");
        services.AddDbContext<ByteShopDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ByteShopDbContext>()
            .AddDefaultTokenProviders();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IAccountService, AccountService>();
    }
}
