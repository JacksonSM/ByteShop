using ByteShop.Application.CommandHandlers.Category;
using ByteShop.Application.CommandHandlers.Product;
using ByteShop.Application.Commands.Category;
using ByteShop.Application.Commands.Product;
using ByteShop.Application.Queries;
using ByteShop.Application.QueryHandlers.Product;
using ByteShop.Application.Reponses;
using ByteShop.Application.Services;
using ByteShop.Application.Services.Contracts;
using ByteShop.Application.Tools;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ByteShop.Application;
public static class Bootstrapper
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddMapping(services);
        AddApplicationServices(services);
        AddCommandHandlers(services);
    }

    private static void AddMapping(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperConfig));
    }

    private static void AddApplicationServices(IServiceCollection services)
    {
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<ICategoryAppService, CategoryAppService>();
        services.AddScoped<IUserAppService, UserAppService>();
    }
    public static void AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<AddProductCommand, ValidationResult>, AddProductHandler>();
        services.AddScoped<IRequestHandler<UpdateProductCommand, ValidationResult>, UpdateProductHandler>();
        services.AddScoped<IRequestHandler<DeleteProductCommand, ValidationResult>, DeleteProductHandler>();

        services.AddScoped<IRequestHandler<AddCategoryCommand, ValidationResult>, AddCategoryHandler>();
        services.AddScoped<IRequestHandler<UpdateCategoryCommand, ValidationResult>, UpdateCategoryHandler>();
        services.AddScoped<IRequestHandler<DeleteCategoryCommand, ValidationResult>, DeleteCategoryHandler>();
    }

    public static void AddQueryHandlers(this IServiceCollection services)
    {

        services.AddScoped<IRequestHandler<GetAllProductsQuery, GetAllProductsResponse>, GetAllProductsHandler>();
    }
}
