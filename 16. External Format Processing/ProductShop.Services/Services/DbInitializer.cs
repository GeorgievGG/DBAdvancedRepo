using ProductShop.Data;
using ProductShop.Services.Contracts;

namespace ProductShop.Services.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ProductShopContext context;

        public DbInitializer(ProductShopContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            context.Database.EnsureCreated();
        }

        public void Reset()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
