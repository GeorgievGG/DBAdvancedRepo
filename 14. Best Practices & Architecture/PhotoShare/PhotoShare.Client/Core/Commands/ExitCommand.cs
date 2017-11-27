using PhotoShare.Client.Core.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] data)
        {
            return "Good Bye!";
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
