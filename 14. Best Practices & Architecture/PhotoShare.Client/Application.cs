namespace PhotoShare.Client
{
    using Core;
    using Data;
    using Microsoft.Extensions.DependencyInjection;
    using PhotoShare.Models;
    using PhotoShare.Services.Contracts;
    using PhotoShare.Services.Services;
    using System;

    public class Application
    {
        public static void Main()
        {
            var col = (Color)4;
            ResetDatabase();

            CommandDispatcher commandDispatcher = new CommandDispatcher();
            var serviceProvider = ConfigureServices();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IDbInitializer, DbInitializerService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }

        private static void ResetDatabase()
        {
            using (var db = new PhotoShareContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
