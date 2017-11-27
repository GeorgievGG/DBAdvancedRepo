using System;
using System.Collections.Generic;
using System.Linq;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

        //Task 4:

        public void PayBills(decimal amountToPay)
        {
            var bankAccounts = this.PaymentMethods
                                    .Where(pm => pm.CreditCardId == null)
                                    .ToList();

            var creditCards = this.PaymentMethods
                                    .Where(pm => pm.BankAccountId == null)
                                    .ToList();

            if (bankAccounts.Select(x => x.BankAccount.Balance).DefaultIfEmpty(0).Sum() + creditCards.Select(x => x.CreditCard.LimitLeft).DefaultIfEmpty(0).Sum() < amountToPay)
            {
                throw new ArgumentException("Insufficient funds!");
            }

            if (bankAccounts.Count() != 0)
            {
                foreach (var ba in bankAccounts)
                {
                    if (amountToPay > 0)
                    {
                        amountToPay -= ba.BankAccount.Withdraw(amountToPay);
                    }
                    else
                    {
                        Console.WriteLine("Payment successful");
                        return;
                    }
                }
            }

            if (creditCards.Count() != 0)
            {
                foreach (var cc in creditCards)
                {
                    if (amountToPay > 0)
                    {
                        amountToPay -= cc.CreditCard.Withdraw(amountToPay);
                    }
                    else
                    {
                        Console.WriteLine("Payment successful");
                        return;
                    }
                }
            }
        }
    }
}
