using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PhotoShare.Client.Core;
using PhotoShare.Data;
using PhotoShare.Services.Contracts;
using PhotoShare.Services.Services;
using System;

namespace PhotoShare.Client
{
    public class Application
    {
        public static void Main()
        {
            var serviceProvider = ConfigureServices();

            CommandDispatcher commandDispatcher = new CommandDispatcher(serviceProvider);
            Engine engine = new Engine(commandDispatcher, serviceProvider);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<PhotoShareContext>(options => options.UseSqlServer("Server=.;Database=PhotoShare;Integrated Security = True"));

            serviceCollection.AddTransient<IDbInitializer, DbInitializerService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IAlbumTagService, AlbumTagService>();
            serviceCollection.AddTransient<IAlbumRoleService, AlbumRoleService>();
            serviceCollection.AddTransient<IAlbumService, AlbumService>();
            serviceCollection.AddTransient<IColorService, ColorService>();
            serviceCollection.AddTransient<IFriendshipService, FriendshipService>();
            serviceCollection.AddTransient<IPictureService, PictureService>();
            serviceCollection.AddTransient<ITagService, TagService>();
            serviceCollection.AddTransient<ITownService, TownService>();

            serviceCollection.AddSingleton<IUserSessionService, UserSessionService>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            return serviceProvider;
        }
    }
}
