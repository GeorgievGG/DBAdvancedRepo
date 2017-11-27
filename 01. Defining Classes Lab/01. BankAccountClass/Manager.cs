using System;
using System.Collections.Generic;

namespace _01.BankAccountClass
{
    public class Manager
    {
        public void InterpretCommand(string cmd, Dictionary<int, BankAccount> accounts)
        {
            var cmdArgs = cmd.Split();
            switch (cmdArgs[0])
            {
                case "Create":
                    Create(cmdArgs, accounts);
                    break;
                case "Deposit":
                    Deposit(cmdArgs, accounts);
                    break;
                case "Withdraw":
                    Withdraw(cmdArgs, accounts);
                    break;
                case "Print":
                    Print(cmdArgs, accounts);
                    break;
                case "End":
                    End();
                    break;
                default:
                    break;
            }
        }

        private void Create(string[] cmdArgs, Dictionary<int, BankAccount> accounts)
        {
            var id = int.Parse(cmdArgs[1]);
            if (accounts.ContainsKey(id))
            {
                Console.WriteLine("Account already exist");
            }
            else
            {
                accounts.Add(id, new BankAccount(id));
            }
        }

        private void Deposit(string[] cmdArgs, Dictionary<int, BankAccount> accounts)
        {
            var id = int.Parse(cmdArgs[1]);
            if (!accounts.ContainsKey(id))
            {
                Console.WriteLine("Account does not exist");
            }
            else
            {
                accounts[id].Balance += decimal.Parse(cmdArgs[2]);
            }
        }

        private void Withdraw(string[] cmdArgs, Dictionary<int, BankAccount> accounts)
        {
            var id = int.Parse(cmdArgs[1]);
            if (!accounts.ContainsKey(id))
            {
                Console.WriteLine("Account does not exist");
            }
            else
            {
                if (accounts[id].Balance < decimal.Parse(cmdArgs[2]))
                {
                    Console.WriteLine("Insufficient balance");
                }
                else
                {
                    accounts[id].Balance -= decimal.Parse(cmdArgs[2]);
                }
            }
        }

        private void Print(string[] cmdArgs, Dictionary<int, BankAccount> accounts)
        {
            var id = int.Parse(cmdArgs[1]);
            if (!accounts.ContainsKey(id))
            {
                Console.WriteLine("Account does not exist");
            }
            else
            {
                Console.WriteLine(accounts[id].ToString());
            }
        }

        private void End()
        {
            Environment.Exit(0);
        }
    }
}
