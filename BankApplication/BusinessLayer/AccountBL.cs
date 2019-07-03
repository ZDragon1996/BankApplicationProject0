using System;
using Entities;
using DAL;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class AccountBL: IAccountBL
    {
        public AccountBL()
        {

           
        }


        public int Accountno { get; set; }
        AccountDAL accountdal = new AccountDAL();

        public List<IAccount> GetAllAccount()
        {
            return accountdal.GetAllAccount();
        }

        public List<IAccount> GetAllAccount(int id)
        {
            return accountdal.GetAllAccount(id);
        }

        public List<IAccount> GetAllAccountsWithoutLoan(int customerId)
        {
            return accountdal.GetAllAccountsWithoutLoan(customerId);
        }


        public void OpenAccount(IAccount account)
        {
            accountdal.OpenAccount(account);
        }

         public void CloseAccount(int accountno)
         {
            accountdal.CloseAccount(accountno);
        }
         public int GenerateAccountno()
         {
            return accountdal.GenerateAccountno();
         }

    

        public string GetAcountType(IAccount account)
        {
            return accountdal.GetAcountType(account);
        }


        public double GetIntrestrate(IAccount account)
        {
            return accountdal.GetIntrestrate(account);
        }


        public decimal GetBalance()
        {
            return accountdal.GetBalance();
        }

        public bool GetAccountStatus()
        {
            return accountdal.GetAccountStatus();
        }

        public void  Deposit(IAccount account, decimal amount) 
        {
            if (amount > 0)
            {
                accountdal.Deposit(account, amount);
            }


        }

        public void Withdraw(IAccount account, decimal amount)
        {
            if (amount > 0)
            {
                accountdal.Withdraw(account, amount);
            }
             
        }

        public bool AcccountIsFound(int accoutno)
        {
            return accountdal.AcccountIsFound(accoutno);
        }

        public void Transfer(int fromAccountno, int toAccountno, decimal amount)
        {
            accountdal.Transfer(fromAccountno, toAccountno, amount);
        }

        public void PayLoan(int accountno, decimal amount)
        {
            accountdal.PayLoan(accountno, amount);
        }

        public IAccount GetAccount(int accountno)
        {
            return accountdal.GetAccount(accountno);
        }

        public List<IAccount> GetAllLoanAccounts(int customerId)
        {
            return accountdal.GetAllLoanAccounts(customerId);
        }

        public List<Transaction> GetTransaction(int accountno)
        {
            return accountdal.GetTransaction(accountno);

        }



     }
}
