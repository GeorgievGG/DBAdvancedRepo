using PhotoShare.Data;
using PhotoShare.Services.Contracts;

namespace PhotoShare.Services.Services
{
    public class DbInitializerService : IDbInitializer
    {
        private readonly PhotoShareContext context;

        public DbInitializerService(PhotoShareContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            this.context.Database.EnsureCreated();
        }

        public void Reset()
        {
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }
    }
}
