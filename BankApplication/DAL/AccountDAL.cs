using System.Collections.Generic;
using Entities;
using System;

namespace DAL
{
    public class AccountDAL
    {
        public AccountDAL()
        {
        }
        private List<IAccount> accounts = new List<IAccount>();
  


        public delegate void TransactionInfo(IAccount account, double amount);
        private static List<int> accountsno = new List<int>();
      




        #region List<IAccount> GetAllAccount()
        public List<IAccount> GetAllAccount()
        {
            //get all accounts without customerId
            return accounts;
        }
        #endregion 

        #region List<IAccount> GetAllAccount(int customerId)
        public List<IAccount> GetAllAccount(int customerId)
        {

            List<IAccount> tempList = new List<IAccount>();
            foreach(var acc in accounts)
            {
                if(acc.CustomerId == customerId && acc.AccountStatus)
                {
                    tempList.Add(acc);
                }
            }

            return tempList;
        }
        #endregion

        #region List<IAccount> GetAllLoanAccounts(int customerId)
        public List<IAccount> GetAllLoanAccounts(int customerId)
        {
            List<IAccount> tempList = new List<IAccount>();
            foreach (var acc in accounts)
            {
                if (acc.CustomerId == customerId && acc.AccountStatus && acc.GetAccountType(acc) == "Loan")
                {
                    tempList.Add(acc);
                }
            }

            return tempList;
        }
        #endregion

        #region List<IAccount> GetAllAccountsWithoutLoan(int customerId)
        public List<IAccount> GetAllAccountsWithoutLoan(int customerId)
        {
            List<IAccount> tempList = new List<IAccount>();
            foreach (var acc in accounts)
            {
                if (acc.CustomerId == customerId && acc.AccountStatus && acc.GetAccountType(acc) != "Loan")
                {
                    tempList.Add(acc);
                }
            }

            return tempList;
        }
        #endregion

        #region void OpenAccount(IAccount account)
        public void OpenAccount(IAccount account)
        {
            accounts.Add(account);
            account.OpenAccount(account);

        }
        #endregion

        #region bool GetAccountStatus()
        public bool GetAccountStatus()
        {
            return true;
        }
        #endregion

        #region double GetBalance()
        public decimal GetBalance()
        {
            return 0M;
        }
        #endregion

        #region void CloseAccount(int accountno)
        public void CloseAccount(int accountno)
        {

             IAccount account = GetAccount(accountno);

            if (AcccountIsFound(accountno))
            {
                if (account.AccountStatus && account.Balance >= 0)
                {
                    account.AccountStatus = false;

                    Console.WriteLine($"Succeed!! Close account no : {account.Accountno}");
                }
                else if (account.AccountStatus)
                {
                    Console.WriteLine($"Failed!! Balance is {account.Balance}");
                }
            }
            else
            {
                Console.WriteLine($"Can not find record");
            }

        }
        #endregion

        #region int GenerateAccountno()
        public int GenerateAccountno()
        {
       
            //int range: -2.147483648 x 10^9 to 2.147483647 x 10^9
            int intMax = int.MaxValue;

            Random random = new Random();
            int randomint = random.Next(1, intMax);

            while (accountsno.Contains(randomint)) //never ending loop if all numbers are taken
            {
                randomint = random.Next(1, intMax);
     
            }

            accountsno.Add(randomint);
            
            return randomint;
        }
        #endregion


        #region string GetAcountType(IAccount account)
        public string GetAcountType(IAccount account)
        {
            return account.AccountType;
        }
        #endregion

        public double GetIntrestrate(IAccount account)
        {
            return account.GetIntrestrate();
        }


        #region int GetCustomerId(Customer customer)
        public int GetCustomerId(Customer customer)
        {
            return customer.CustomerId;
        }
        #endregion

        #region void Deposit(IAccount account, decimal amount)
        public void Deposit(IAccount account, decimal amount)
        {
       
            IAccount acc = GetAccount(account.Accountno);

            if (acc.GetAccountType(acc) == "Loan")
            {
                Console.WriteLine("Can not deposit to Loan Account");
            }
            else if(acc.GetAccountType(acc) == "Term Deposit")
            {
                Console.WriteLine("Can not deposit to Term Deposit");
            }
            else if (acc.AccountStatus && amount > 0)
            {
                acc.Balance += amount;
                if (!Account.IsTransfer)
                {
                    Transaction.CreateTransaction(account, account, amount, "Deposit");
                }
            }
            else 
            {
                Console.WriteLine("Faield, amount is possibly less or equal to 0");
            }

        }
        #endregion


        #region void Withdraw(IAccount account,  decimal amount)
        public void Withdraw(IAccount account, decimal amount)
        {
            //IAccount acc = GetAccount(account.Accountno);
            if(amount < 0)
            {
                Console.WriteLine("Negative");
            }
            else
            {
                account.Withdraw(account, amount);
            }
          
        }
        #endregion

        #region bool AcccountIsFound(int accountno)
        public bool AcccountIsFound(int accountno)
        {
            bool status = false;
            foreach(var acc in accounts)
            {
                if(acc.Accountno == accountno && acc.AccountStatus)
                {
                    status = true;
                }
            }
            return status;
            
        }
        #endregion


        #region void Transfer(int fromAccountno, int toAccountno, decimal amount)
        public void Transfer(int fromAccountno, int toAccountno, decimal amount)
        {
            Account.IsTransfer = true;
          
            IAccount firstAccount = GetAccount(fromAccountno);
            IAccount secondAccount = GetAccount(toAccountno);
            bool withdrawSucceed = true;

            if(firstAccount.Accountno != secondAccount.Accountno)
            {
                if ((firstAccount.GetAccountType(firstAccount) == "Loan" || secondAccount.GetAccountType(secondAccount) == "Loan") && firstAccount.AccountStatus)
                {
                    //checking from if tranfer from loan account
                    
                    Console.WriteLine("Unable transfer to Loan account");

                }

                if (secondAccount.GetAccountType(secondAccount) == "Loan" && (firstAccount.AccountStatus && secondAccount.AccountStatus))
                {
                    //checking from if tranfer to loan account
                }
                else if (firstAccount.AccountStatus && secondAccount.GetAccountType(secondAccount) != "Term Deposit" && amount >= 0)
                {
                    //checking if first account and second account are not term deposit
                    if (firstAccount.GetAccountType(firstAccount) == "Checking" && (firstAccount.Balance - amount) < 0)
                    {
                        //checking account amount must greater than 0
                        withdrawSucceed = false; //  no withdraw any money from account
                    }


                    firstAccount.Withdraw(firstAccount, amount); // process depend on the account

                }
                else
                {
                    Console.WriteLine($"Transfer to termdeposit is not possible");
                }




                if ((firstAccount.AccountStatus && secondAccount.AccountStatus) && withdrawSucceed && (firstAccount.GetAccountType(firstAccount)== "Checking" || firstAccount.GetAccountType(firstAccount) == "Business") && (secondAccount.GetAccountType(secondAccount) == "Checking" || secondAccount.GetAccountType(secondAccount) == "Business") && amount >= 0)
                {
                    Console.WriteLine($"Deposit {amount} to the accountno: {secondAccount.Accountno}");
                    Deposit(secondAccount, amount);
                    Transaction.CreateTransaction(firstAccount, secondAccount, amount, "Transfer");
                    Transaction.CreateTransaction(firstAccount, secondAccount, amount, "Received");
                }

            }//end   if(firstAccount.Accountno != secondAccount.Accountno)

            else
            {
                Console.WriteLine("Unable transfer to the same acocunt");
            }

            

        }//end
        #endregion

        #region void PayLoan(int accountno, decimal amount)
        public void PayLoan(int accountno, decimal amount)
        {
            var account = GetAccount(accountno);
            account.Balance -= amount;
        }
        #endregion


        #region IAccount GetAccount(int accountno)
        public IAccount GetAccount(int accountno)
        {
            IAccount account = new Account();
            foreach(var acc in accounts)
            {
                if(acc.Accountno == accountno)
                {
                    account = acc;
                }
            }
            return account;
        }
        #endregion

        #region List<Transaction> GetTransaction(int accountno)

        public List<Transaction> GetTransaction(int accountno)
        {
            List<Transaction> tempTrans = new List<Transaction>();
            foreach(var tran in Transaction.transactions)
            {
                if(tran.AccountNo == accountno)
                {
                    tempTrans.Add(tran);
                }
            }
            return tempTrans;
        }
        #endregion



    }
}
