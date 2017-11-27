namespace _01.BankAccountClass
{
    public class BankAccount
    {
        public int ID { get; set; }
        public decimal Balance { get; set; }

        public BankAccount(int id)
        {
            this.ID = id;
            this.Balance = 0;
        }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            this.Balance -= amount;
        }

        public override string ToString()
        {
            return $"Account ID{this.ID}, balance {this.Balance:f2}";
        }
    }
}
