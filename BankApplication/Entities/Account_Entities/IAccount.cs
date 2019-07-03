using System;
using System.Collections.Generic;

namespace Entities
{
    public interface IAccount
    {
        
         //properties
        //List<Account> getAllAccount(uint id);
        string AccountType { get; set; }
        int Accountno { get; set; }

        decimal Balance { get; set; }
        bool AccountStatus { get; set; }

        int CustomerId { get; set; }
        Customer customerinfo { get; set; } // account belong to 1 customer
        double Interestrate { get; set; }

        List<Transaction> transactions { get; set; }
        

        //methods
        void OpenAccount(IAccount account);
        void CloseAccount(int id);
        string GetAccountType(IAccount account);
        void Withdraw(IAccount account, decimal amount);
        void Deposit(int accountno, decimal amount);
        void Transfer(int fromAccount, int toAccount, decimal amount);

        double GetIntrestrate();


    }
}
