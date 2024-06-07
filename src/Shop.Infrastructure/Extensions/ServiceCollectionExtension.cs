using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Entities;
using Shop.Domain.Interfaces;
using Shop.Domain.Repositories;
using Shop.Infrastructure.Authorization.Services;
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

            services.AddIdentityCore<User>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ShopDbContext>();

            //services.AddDefaultIdentity<User>()
            //        .AddRoles<IdentityRole>()
            //        .AddEntityFrameworkStores<ShopDbContext>()
            //        .AddDefaultTokenProviders();

            services.AddScoped<IShopSeeder, ShopSeeder>();
            
            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddScoped<IProductCategoriesRepository, ProductCategoriesRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();

            services.AddScoped<IOrderStatusesRepository, OrderStatusesRepository>();
            services.AddScoped<IOrdersRepository, OrdersRepository>();  

            services.AddScoped<IOrderAuthorizationService, OrderAuthorizationService>();
        }
    }
}
