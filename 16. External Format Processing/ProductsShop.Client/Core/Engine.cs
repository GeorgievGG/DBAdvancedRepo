using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Models;
using ProductShop.Services.Contracts;
using System;
using System.IO;

namespace ProductsShop.Client.Core
{
    public class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            Console.WriteLine("Initializing database...");
            var dbInitializer = (IDbInitializer)serviceProvider.GetService(typeof(IDbInitializer));
            Console.WriteLine("Database initialized!");
            dbInitializer.Initialize();
            //var context = (ProductShopContext)serviceProvider.GetService(typeof(ProductShopContext));
            //Console.WriteLine("Seeding data...");
            //SeedDbContextWithData(context);
            //Console.WriteLine("Data successfully inserted!");
        }

        private static void SeedDbContextWithData(ProductShopContext context)
        {
            User[] users = ImportUsersFromJson(context);
            var rnd = new Random();
            Product[] products = ImportProductsFromJson(context, users, rnd);
            Category[] categories = ImportCategoriesFromJson(context);
            ImportCategoryProductsFromJson(context, rnd, products, categories);
        }

        private static void ImportCategoryProductsFromJson(ProductShopContext context, Random rnd, Product[] products, Category[] categories)
        {
            for (int i = 0; i < products.Length; i++)
            {
                var randomCategoryId = rnd.Next(1, categories.Length + 1);
                var categoryProd = new CategoryProduct
                {
                    CategoryId = randomCategoryId,
                    ProductId = products[i].Id
                };
                context.CategoryProducts.Add(categoryProd);
            }
            context.SaveChanges();
        }

        private static Category[] ImportCategoriesFromJson(ProductShopContext context)
        {
            var categories = JsonImport<Category>("Files/categories.json");
            context.Categories.AddRange(categories);
            context.SaveChanges();
            return categories;
        }

        private static Product[] ImportProductsFromJson(ProductShopContext context, User[] users, Random rnd)
        {
            var products = JsonImport<Product>("Files/products.json");
            for (int i = 0; i < products.Length; i++)
            {
                var randomId = rnd.Next(0, users.Length + 1);
                var secondRandomId = rnd.Next(1, users.Length + 1);
                if (randomId == 0)
                {
                    products[i].BuyerId = null;
                }
                else
                {
                    products[i].BuyerId = randomId;
                }
                products[i].SellerId = secondRandomId;
            }
            context.Products.AddRange(products);
            context.SaveChanges();
            return products;
        }

        private static User[] ImportUsersFromJson(ProductShopContext context)
        {
            var users = JsonImport<User>("Files/users.json");
            context.Users.AddRange(users);
            context.SaveChanges();
            return users;
        }

        public static T[] JsonImport<T>(string path)
        {
            string jsonString = File.ReadAllText(path);

            var objects = JsonConvert.DeserializeObject<T[]>(jsonString);
            return objects;
        }
    }
}
