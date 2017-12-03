using AutoMapper;
using EmployeeDB.Client.Core;
using EmployeeDB.Data;
using EmployeeDB.Data.Configurations;
using EmployeeDB.Services.Contracts;
using EmployeeDB.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EmployeeDB.Client
{
    public class StartUp
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();
            var engine = new Engine(serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<EmployeeContext>(options => options.UseSqlServer(DBConfiguration.ConnectionString));

            serviceCollection.AddTransient<IDbInitializerService, DbInitializerService>();
            serviceCollection.AddTransient<IEmployeeService, EmployeeService>();

            serviceCollection.AddAutoMapper();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
