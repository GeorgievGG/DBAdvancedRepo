using System;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }
        public decimal Balance { get; private set; }
        public string BankName { get; set; }
        public string SwiftCode { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal Withdraw(decimal sum)
        {
            if (sum <= this.Balance)
            {
                this.Balance -= sum;
                return sum;
            }
            else
            {
                this.Balance = 0;
                return sum -= Balance;
            }
        }

        public void Deposit(decimal sum)
        {
            this.Balance += sum;
        }
    }
}
