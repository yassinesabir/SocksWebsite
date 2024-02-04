using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SocksWebsite.Data.Enums;
using SocksWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocksWebsite.Data.Static;

namespace SocksWebsite.Data
{
    public class AppDbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // Your existing seeding logic using the AppDbContext instance
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                {
                    new Product()
                    {
                        Name = "Socks1",
                        Description = "This is the Socks1 product description",
                        Quantity = 5,
                        ImageURL = "/img/S1.jpg",
                        ProductCategory = ProductCategory.Man,
                        ProductSubCategory = ProductSubCategory.FashionSocks,
                        Price = 80.00,
                        ProductSize = ProductSize.Size30to34,
                    },
                    new Product()
                        {
                            Name = "Socks2",
                            Description = "This is the Socks2 description",
                            Quantity = 10,
                            ImageURL = "/img/S2.jpg",
                            ProductCategory = ProductCategory.Man,
                            ProductSubCategory = ProductSubCategory.CottonSocks,
                            Price = 65.00,
                            ProductSize = ProductSize.Size38to40,
                        },
                        new Product()
                        {
                            Name = "Socks3",
                            Description = "This is the Socks1 product description",
                            Quantity = 15,
                            ImageURL = "/img/S3.jpg",
                            ProductCategory = ProductCategory.Sales,
                            ProductSubCategory = ProductSubCategory.CottonSocks,
                            Price = 50.00,
                            ProductSize = ProductSize.Size36to38,
                        },
                        new Product()
                        {
                            Name = "Socks4",
                            Description = "This is the Socks1 product description",
                            Quantity = 2,
                            ImageURL = "/img/S4.jpg",
                            ProductCategory = ProductCategory.Woman,
                            ProductSubCategory = ProductSubCategory.FashionSocks,
                            Price = 95.00,
                            ProductSize = ProductSize.Size30to34,
                        },
                        new Product()
                        {
                            Name = "Socks5",
                            Description = "This is the Socks1 product description",
                            Quantity = 2,
                            ImageURL = "/img/S5.jpg",
                            ProductCategory = ProductCategory.Woman,
                            ProductSubCategory = ProductSubCategory.SummerSocks,
                            Price = 95.00,
                            ProductSize = ProductSize.Size42to44,
                        },
                });

                context.SaveChanges();
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "hasna.ettaki16@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Socks123.");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "saberyaser@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Socks123.");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
