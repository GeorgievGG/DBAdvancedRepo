using EmployeeDB.Client.Commands.Contracts;
using System;

namespace EmployeeDB.Client.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] data)
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);

            return "";
        }
    }
}
