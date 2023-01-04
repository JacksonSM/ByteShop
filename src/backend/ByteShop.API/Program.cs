using ByteShop.API.Filters;
using ByteShop.Application;
using ByteShop.Domain.Account;
using ByteShop.Infra.CrossCutting;
using ByteShop.Infra.CrossCutting.Bus;
using ByteShop.Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(option => option.LowercaseUrls = true);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCommandHandlers();
builder.Services.AddMediatorServices();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ByteShop"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionsFilter)));

var logger = Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

//SeedUserRoles(app);

app.UseCors(cfg =>
{
    cfg.AllowAnyOrigin()
        .AllowAnyMethod()
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .WithExposedHeaders("X-Pagination");
});
app.MapControllers();

app.Run();
async void SeedUserRoles(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var seed = serviceScope.ServiceProvider
                               .GetService<ISeedUserRoleInitial>();
        await seed.SeedRoles();
        await seed.SeedUsers();
    }
}