using Microsoft.AspNetCore.Identity;
using Shop.Domain.Constants;
using Shop.Domain.Entities;
using Shop.Infrastructure.Persistence;

namespace Shop.Infrastructure.Seeders
{
    internal class ShopSeeder(ShopDbContext dbContext) : IShopSeeder
    {
        public async Task Seed()
        {
            bool isSaveConnection = await dbContext.Database.CanConnectAsync();

            if(isSaveConnection)
            {
                var isRolesEmpty = !dbContext.Roles.Any();

                if(isRolesEmpty)
                {
                    var roles = GetRoles();
                    dbContext.Roles.AddRange(roles);
                    await dbContext.SaveChangesAsync();
                }
                    
                var isUsersEmpty = !dbContext.Users.Any();  

                if(isUsersEmpty)
                {
                    var users = GetUsers();
                    dbContext.Users.AddRange(users);
                    await dbContext.SaveChangesAsync();
                }

                var isProductCategoriesEmpty = !dbContext.ProductCategories.Any();
                var isProductsEmpty = !dbContext.Products.Any();
                var isOrderStatusesEmpty = !dbContext.OrderStatuses.Any();
                var isOrdersEmpty = !dbContext.Orders.Any();

                if(isProductCategoriesEmpty && isProductsEmpty && isOrderStatusesEmpty &&isOrdersEmpty)
                {
                    var orders = GetOrders();
                    dbContext.Orders.AddRange(orders);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<IdentityRole> GetRoles() 
        {
            List<IdentityRole> roles = 
            [
                new IdentityRole(UserRoles.Admin) 
                {
                    NormalizedName = UserRoles.Admin.ToUpper(),
                },
                new IdentityRole(UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper(),

                },
                new IdentityRole(UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper(),
                }
            ];

            return roles;
        }

        private IEnumerable<User> GetUsers()
        {
            var admin = new User
            {
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN"
            };

            var user = new User
            {
                Email = "kylianmbapper@gmail.com",
                EmailConfirmed = true,
                UserName = "kylian",
                NormalizedEmail = "KYLIAN"
            };

            // Hash admin password
            PasswordHasher<User> adminPh = new PasswordHasher<User>();
            admin.PasswordHash = adminPh.HashPassword(admin, "zaq1@WSX");
                
            // Hash user password
            PasswordHasher<User> userPh = new PasswordHasher<User>();
            user.PasswordHash = userPh.HashPassword(user, "zaq1@WSX");

            List<User> users = [admin, user];

            return users;
        }

        private IEnumerable<Order> GetOrders()
        {
            var user = dbContext.Users.First();

            if (user == null)
            {
                return Enumerable.Empty<Order>();
            }

            List<Order> orders = new List<Order>()
            {
                new Order()
                {
                    ProductQuantity = 1,
                    OrderStatus = new OrderStatus() { Name = "Preparation" },
                    OrderedById = user.Id,
                    Product = new Product()
                    {
                        Name = "Iphone 13",
                        Description = "The iPhone 13, introduced in 2021, is part of Apple's flagship smartphone series.",
                        Price = 699.99M,
                        Quantity = 100,
                        ProductCategory = new ProductCategory() { Name = "Electronics" }
                    }
                },
                new Order() 
                {
                    ProductQuantity = 2, 
                    OrderStatus = new OrderStatus() { Name = "Shipped" },
                    OrderedById = user.Id,
                    Product = new Product()
                    {
                        Name = "Nike sports sweatshirt",
                        Description = "Elevate your athletic wardrobe with the Nike Dri-FIT Performance Crewneck Sweatshirt, a perfect blend of style and functionality.",
                        Price = 24.99M,
                        Quantity = 100,
                        ProductCategory = new ProductCategory() { Name = "Fashion" }
                    },
                },
                new Order() 
                { 
                    ProductQuantity = 3,
                    OrderStatus = new OrderStatus() { Name = "Delivered" },
                    OrderedById = user.Id,
                    Product = new Product()
                    {
                        Name = "Game of Thrones - A Clash of Kingdoms",
                        Description = "Dive into the epic realm of Westeros with 'A Clash of Kingdoms,' the latest installment in the gripping 'Game of Thrones' series by George R.R. Martin.",
                        Price = 9.99M,
                        Quantity = 100,
                        ProductCategory = new ProductCategory() { Name = "Entertainment" }
                    }
                }
            };

            return orders;
        }
    }
}
