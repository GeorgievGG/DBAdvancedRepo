using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ProductShop.Data;
using ProductShop.Data.Configurations;
using ProductShop.Models;
using ProductShop.Services.Contracts;
using ProductShop.Services.Services;
using ProductsShop.Client.Core;
using System;
using System.IO;

namespace ProductsShop.Client
{
    public class StartUp
    {
        public static void Main()
        {
            var serviceProvider = Configure();
            var engine = new Engine(serviceProvider);
            engine.Run();
            
        }

        public static IServiceProvider Configure()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<ProductShopContext>(options => options.UseSqlServer(DbConfig.ConnectionString));
            serviceCollection.AddTransient<IDbInitializer, DbInitializer>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
