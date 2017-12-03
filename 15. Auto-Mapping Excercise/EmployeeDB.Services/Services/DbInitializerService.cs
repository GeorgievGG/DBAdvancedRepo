using EmployeeDB.Data;
using EmployeeDB.Services.Contracts;

namespace EmployeeDB.Services.Services
{
    public class DbInitializerService : IDbInitializerService
    {
        private readonly EmployeeContext context;

        public DbInitializerService(EmployeeContext context)
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
