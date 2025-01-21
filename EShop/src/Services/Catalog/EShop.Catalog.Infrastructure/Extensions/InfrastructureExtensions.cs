using EShop.Catalog.Application.Contracts;
using EShop.Catalog.Application.Features.Products.Queries;
using EShop.Catalog.Infrastructure.EventHandlers;
using EShop.Catalog.Infrastructure.Persistence;
using EShop.Catalog.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Catalog.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration) {

            var connectionString = configuration.GetConnectionString("db");
            var host = configuration["DefaultHostName"];
            var pass = configuration["DefaultPassword"];

            connectionString = connectionString.Replace("[PASS]", pass).Replace("[HOST]", host);

            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString, b=>b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName)));
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<GetAllProductsQueryHandler>();
                config.RegisterServicesFromAssemblyContaining<ProductPriceDiscountDomainEventHandler>();
            });
            return services;
        }
    }

    public static class  DatabaseInitializer
    {
        public static async Task CreateDatabaseAsync(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<CatalogDbContext>();
            await context.Database.MigrateAsync();
        }        
    }
}
