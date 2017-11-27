using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_BillsPaymentSystem.Data.Models
{
    public class CreditCard
    {
        public int CreditCardId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public decimal Limit { get; private set; }
        public decimal MoneyOwed { get; private set; }
        [NotMapped]
        public decimal LimitLeft => Limit - MoneyOwed;

        public static void HasKey(Func<object, object> p)
        {
            throw new NotImplementedException();
        }

        public PaymentMethod PaymentMethod { get; set; }

        public decimal Withdraw(decimal sum)
        {
            if (sum <= this.LimitLeft)
            {
                this.MoneyOwed += sum;
                return sum;
            }
            else
            {
                this.MoneyOwed = Limit;
                return sum -= LimitLeft;
            }
        }

        public void Deposit(decimal sum)
        {
            this.MoneyOwed -= sum;
        }
    }
}
