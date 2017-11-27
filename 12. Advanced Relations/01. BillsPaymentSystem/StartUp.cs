using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        static void Main()
        {
            var dbc = new BillsPaymentSystemContext();
            using (dbc)
            {
                //Task 2:

                //dbc.Database.EnsureDeleted();
                //dbc.Database.Migrate();
                //Seed(dbc);

                //Task 3:

                //var reqUserId = int.Parse(Console.ReadLine());

                //var user = dbc.Users
                //            .FirstOrDefault(u => u.UserId == reqUserId);

                //try
                //{
                //    Console.WriteLine($"User: {user.FirstName} {user.LastName}");

                //    var bankAccounts = dbc.PaymentMethods
                //                            .Where(pm => pm.UserId == reqUserId)
                //                            .Include(pm => pm.CreditCard)
                //                            .Where(pm => pm.CreditCard == null)
                //                            .Include(pm => pm.BankAccount);
                //    if (bankAccounts.Count() != 0)
                //    {
                //        Console.WriteLine("Bank Accounts:");
                //        foreach (var ba in bankAccounts)
                //        {
                //            Console.WriteLine($"-- ID: {ba.BankAccountId}");
                //            Console.WriteLine($"--- Balance: {ba.BankAccount.Balance:f2}");
                //            Console.WriteLine($"--- Bank: {ba.BankAccount.BankName}");
                //            Console.WriteLine($"--- SWIFT: {ba.BankAccount.SwiftCode}");
                //        }
                //    }

                //    var creditCards = dbc.PaymentMethods
                //                            .Where(pm => pm.UserId == reqUserId)
                //                            .Include(pm => pm.BankAccount)
                //                            .Where(pm => pm.BankAccount == null)
                //                            .Include(pm => pm.CreditCard);
                //    if (creditCards.Count() != 0)
                //    {
                //        Console.WriteLine("Credit Cards:");
                //        foreach (var cc in creditCards)
                //        {
                //            Console.WriteLine($"-- ID: {cc.CreditCardId}");
                //            Console.WriteLine($"--- Limit: {cc.CreditCard.Limit:f2}");
                //            Console.WriteLine($"--- Money Owed: {cc.CreditCard.MoneyOwed:f2}");
                //            Console.WriteLine($"--- Limit Left: {cc.CreditCard.LimitLeft:f2}");
                //            Console.WriteLine($"--- Expiration Date: {cc.CreditCard.ExpirationDate.ToString("yyyy/MM", CultureInfo.InvariantCulture)}");
                //        }
                //    }
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e.Message);
                //    return;
                //}

                //Task 4

                var reqUserId = int.Parse(Console.ReadLine());
                var reqAmountToPay = decimal.Parse(Console.ReadLine());

                var user = dbc.Users
                            .Include(u => u.PaymentMethods)
                            .ThenInclude(navigationPropertyPath: pm => pm.CreditCard)
                            .Include(u => u.PaymentMethods)
                            .ThenInclude(navigationPropertyPath: pm => pm.BankAccount)
                            .FirstOrDefault(u => u.UserId == reqUserId);
                try
                {
                    user.PayBills(reqAmountToPay);
                    dbc.SaveChanges();
                    Console.WriteLine($"User {reqUserId} paid ${reqAmountToPay:f2}  successfully.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

            }
        }

        //public static void Seed(BillsPaymentSystemContext dbc)
        //{
        //    var users = new List<User>()
        //    {
        //        new User() { FirstName = "Gosho", LastName = "Petrov", Email = "bezmail@gmail.com", Password = "blabla123" },
        //        new User() { FirstName = "Ivan", LastName = "Kirilov", Email = "ik231@gmail.com", Password = "sdasad23" },
        //        new User() { FirstName = "Stepan", LastName = "Kolev", Email = "sk3912@gmail.com", Password = "sfa0921" },
        //        new User() { FirstName = "Fabio", LastName = "Saloni", Email = "fasal@gmail.com", Password = "blabla123" },
        //        new User() { FirstName = "Temil", LastName = "Mangalov", Email = "sfiasfl@gmail.com", Password = "ser819" }
        //    };

        //    dbc.Users.AddRange(users);

        //    var bankAccounts = new List<BankAccount>()
        //    {
        //        new BankAccount() { BankName = "FiBank", SwiftCode = "FWSBQ", Balance = 0 },
        //        new BankAccount() { BankName = "SiBank", SwiftCode = "SIBQS", Balance = 0 },
        //        new BankAccount() { BankName = "KTB", SwiftCode = "BOIKO", Balance = 0 },
        //        new BankAccount() { BankName = "KTB", SwiftCode = "BOIKO", Balance = 0 },
        //    };

        //    dbc.BankAccounts.AddRange(bankAccounts);

        //    var creditCards = new List<CreditCard>()
        //    {
        //        new CreditCard() { Limit = 2000, MoneyOwed = 1000, ExpirationDate = DateTime.Now.AddDays(360) },
        //        new CreditCard() { Limit = 5000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddDays(100) },
        //        new CreditCard() { Limit = 8000, MoneyOwed = 0, ExpirationDate = DateTime.Now.AddDays(180) }
        //    };

        //    dbc.CreditCards.AddRange(creditCards);

        //    dbc.SaveChanges();

        //    var paymentMethods = new List<PaymentMethod>()
        //    {
        //        new PaymentMethod { BankAccountId = 1, CreditCardId = null, Type = PaymentMethodType.BankAccount, UserId = 1 },
        //        new PaymentMethod { BankAccountId = 2, CreditCardId = null, Type = PaymentMethodType.BankAccount, UserId = 3 },
        //        new PaymentMethod { BankAccountId = null, CreditCardId = 2, Type = PaymentMethodType.CreditCard, UserId = 2 },
        //        new PaymentMethod { BankAccountId = null, CreditCardId = 1, Type = PaymentMethodType.CreditCard, UserId = 4 },
        //        new PaymentMethod { BankAccountId = 4, CreditCardId = null, Type = PaymentMethodType.BankAccount, UserId = 5 },
        //        //new PaymentMethod { BankAccountId = 4, CreditCardId = null, Type = PaymentMethodType.BankAccount, UserId = 5 }
        //    };

        //    dbc.PaymentMethods.AddRange(paymentMethods);

        //    dbc.SaveChanges();
        //}
    }
}
