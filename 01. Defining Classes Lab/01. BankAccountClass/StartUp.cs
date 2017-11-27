using System;
using System.Collections.Generic;

namespace _01.BankAccountClass
{
    public class StartUp
    {
        public static void Main()
        {
            var accounts = new Dictionary<int, BankAccount>();
            var command = string.Empty;
            var mgr = new Manager();

            while ((command = Console.ReadLine()) != null)
            {
                mgr.InterpretCommand(command, accounts);
            }
        }
    }
}
