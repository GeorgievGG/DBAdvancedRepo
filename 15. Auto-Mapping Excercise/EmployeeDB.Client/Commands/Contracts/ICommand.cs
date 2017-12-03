namespace EmployeeDB.Client.Commands.Contracts
{
    public interface ICommand
    {
        string Execute(string[] data);
    }
}
