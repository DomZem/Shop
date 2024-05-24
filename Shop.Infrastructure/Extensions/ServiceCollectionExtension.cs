using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Seeders;

namespace Shop.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("ShopDb");
            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IShopSeeder, ShopSeeder>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }
    }
}
