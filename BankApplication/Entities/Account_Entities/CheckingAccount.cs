                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                using System;
using System.Collections.Generic;

namespace Entities
{
    public class CheckingAccount : Account, ICheckingAccount//must inherited account class before interfaces.
    {
        public CheckingAccount()
        {

        }

      
        //private properties
        private static List<IAccount> checkingAccounts = new List<IAccount>();







        public override void OpenAccount(IAccount account)
        {
            checkingAccounts.Add(account);
        }



        public override void Withdraw(IAccount account, decimal amount)
        {

        
            foreach(var acc in checkingAccounts)
            {
                if(acc.Accountno == account.Accountno && (acc.Balance - amount) >= 0 && acc.AccountStatus)
                {
                    if (!IsTransfer)
                    {
                        Transaction.CreateTransaction(account, account, amount, "Withdraw");
                    }
                    acc.Balance -= amount;

                }
                else if (acc.Accountno == account.Accountno && acc.AccountStatus)
                {
                    Console.WriteLine("Insufficient fund, Please try again!");
                }
               
               
            }
         

        }

        //public override List<IAccount> getAllAccount(uint id)
        //{

           

        //}
     

        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Checking";
        }

        public override double GetIntrestrate()
        {
            return Interestrate = 0.02;
        }


        public List<IAccount> GetAllAccounts(uint id)
        {
            List<IAccount> tempList = new List<IAccount>();

            foreach(var acc in checkingAccounts)
            {
                if(acc.CustomerId == id)
                {
                    tempList.Add(acc);
                }
            }
            return tempList;
        }


 

    }

}
