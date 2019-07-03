using System;
using System.Collections.Generic;

namespace Entities
{
    public class Account : IAccount
    {
        public decimal Balance { get; set; }
        public int Accountno { get; set; }
       
        public string AccountType { get; set; }
        public int CustomerId { get; set; }

        public bool AccountStatus { get; set; }

        public Customer customerinfo { get; set; }

        public List<Transaction> transactions { get; set; }

        public static bool IsTransfer { get; set; } = false;


        protected static List<IAccount> accounts = new List<IAccount>();

        public double Interestrate { get; set; }

        public Account()
        {
            AccountStatus = true;
       
        }

        public virtual void OpenAccount(IAccount account)
        {
            Console.WriteLine("Base OpenAccount");

        }

        public virtual void CloseAccount(int id)
        {
            Console.WriteLine("Base CloseAccount");
        }

        public virtual double GetIntrestrate()
        {
            return Interestrate;
        }



        public virtual string GetAccountType()
        {
            return AccountType;
        }

        public decimal GetBalance()
        {
            return Balance;
        }

        public virtual void Withdraw(IAccount account, decimal amount)
        {
            Console.WriteLine("Base Withdraw");
        }

        public virtual void Deposit(int accountno, decimal amount)
        {
            Console.WriteLine("Base Deposit");
        }



        public virtual string GetAccountType(IAccount account)
        {
            Console.WriteLine("Base GetAccountType");
            return "Base of Account";
        }


        public virtual void Transfer(int fromAccount, int toAccount, decimal amount)
        {
            Console.WriteLine("Base Transfer");
        }


      
    }
}
