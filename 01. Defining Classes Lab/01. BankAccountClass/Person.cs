using System.Collections.Generic;
using System.Linq;

namespace _01.BankAccountClass
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<BankAccount> Accounts { get; set; }

        public Person(string name, int age) : this(name, age, new List<BankAccount>())
        {
        }

        public Person(string name, int age, List<BankAccount> accounts)
        {
            this.Name = name;
            this.Age = age;
            this.Accounts = accounts;
        }

        public decimal GetBalance()
        {
            return this.Accounts.Sum(x => x.Balance);
        }
    }
}
