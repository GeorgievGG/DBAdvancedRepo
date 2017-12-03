namespace EmployeeDB.Services.Contracts
{
    public interface IDbInitializerService
    {
        void Initialize();

        void Reset();
    }
}
