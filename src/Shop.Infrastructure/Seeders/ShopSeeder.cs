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

                    // assign roles to users
                    dbContext.UserRoles.AddRange(
                        new IdentityUserRole<string>()
                        {
                            UserId = "1481d8da-89e3-4451-a2ff-02ab11249679",
                            RoleId = "cad52c90-c333-4e8e-8047-fb1f13c069e1",
                        },
                        new IdentityUserRole<string>()
                        {
                            UserId = "2be03688-814d-45c9-8821-31a00f181cea",
                            RoleId = "34b6e195-78c6-4aba-b3c0-da41435ce3ad",
                        }
                    );
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
                    Id = "cad52c90-c333-4e8e-8047-fb1f13c069e1",
                    NormalizedName = UserRoles.Admin.ToUpper(),
                },
                new IdentityRole(UserRoles.User)
                {
                    Id = "34b6e195-78c6-4aba-b3c0-da41435ce3ad",
                    NormalizedName = UserRoles.User.ToUpper(),
                },
                new IdentityRole(UserRoles.Owner)
                {
                    Id = "e0358f57-c10b-4d43-b313-e76f27c60875",
                    NormalizedName = UserRoles.Owner.ToUpper(),
                }
            ];

            return roles;
        }

        private IEnumerable<User> GetUsers()
        {
            var admin = new User
            {
                Id = "1481d8da-89e3-4451-a2ff-02ab11249679",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "admin",
            };

            // Hash admin password
            PasswordHasher<User> adminPh = new PasswordHasher<User>();
            admin.PasswordHash = adminPh.HashPassword(admin, "zaq1@WSX");

            admin.NormalizedUserName = admin.Email.ToUpper();
            admin.NormalizedEmail = admin.Email.ToUpper();
            admin.LockoutEnabled = true;
            admin.LockoutEnd = null;

            var user = new User
            {
                Id = "2be03688-814d-45c9-8821-31a00f181cea",
                Email = "kylianmbappe@gmail.com",
                EmailConfirmed = true,
                UserName = "Kylain",
            };

            // Hash user password
            PasswordHasher<User> userPh = new PasswordHasher<User>();
            user.PasswordHash = userPh.HashPassword(user, "zaq1@WSX");

            user.NormalizedUserName = user.Email.ToUpper();
            user.NormalizedEmail = user.Email.ToUpper();
            user.LockoutEnabled = true;
            user.LockoutEnd = null;

            List<User> users = [admin, user];

            return users;
        }

        private IEnumerable<Order> GetOrders()
        {
            var user = dbContext.Users.Where(u => u.Email == "kylianmbappe@gmail.com").ToList().First();

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
                    TotalPrice = 1 * 699.99M,
                    OrderAddress = new OrderAddress()
                    {
                        Street = "456 Oak Avenue", 
                        City = "Sometown", 
                        PostalCode = "67890", 
                        Country = "Canada"
                    },
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
                    TotalPrice = 2 * 24.99M,
                    OrderAddress = new OrderAddress()
                    {
                        Street = "789 Pine Lane", 
                        City = "Birmingham", 
                        PostalCode = "54321", 
                        Country = "UK"
                    },
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
                    TotalPrice = 3 * 9.99M,
                    OrderAddress = new OrderAddress()
                    {
                        Street = "123 Main Street", 
                        City = "Anytown",
                        PostalCode = "12345", 
                        Country = "USA"
                    },
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
