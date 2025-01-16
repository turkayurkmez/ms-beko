using EShop.Catalog.Application.Contracts;
using EShop.Catalog.Application.Features.Products.Queries;
using EShop.Catalog.Infrastructure.EventHandlers;
using EShop.Catalog.Infrastructure.Persistence;
using EShop.Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddDbContext<CatalogDbContext>(options => options.UseSqlServer(connectionString));
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
}
