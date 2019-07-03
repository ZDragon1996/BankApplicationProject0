using System;

using System.Collections.Generic;

namespace Entities
{
    public class BusinessAccount : Account, IBusinessAccount//must inherited account class before interfaces.
    {
        public BusinessAccount()
        {

        }



        //private properties
 

        private static List<IAccount> businessAccounts = new List<IAccount>();



        //methods
        public override void OpenAccount(IAccount account)
        {

            businessAccounts.Add(account); 



        }//end method OpenAccount()

        public void CloseAccount()
        {
           
        }//end method CloseAccount()




        public override void Withdraw(IAccount account, decimal amount)
        {
            foreach (var acc in businessAccounts)
            {
                if (acc.Accountno == account.Accountno && acc.AccountStatus)
                {
                    acc.Balance -= amount;
                    if (!IsTransfer)
                    {
                        Transaction.CreateTransaction(account, account, amount, "Withdraw");
                    }
                   

                }
       
                else
                {
                    Console.WriteLine("Record not found");
                }

            }


        }


        //public void Transfer(int fromAccount, int toAccount, long amount)
        //{

        //}

        public List<IAccount> getAll()
        {


            foreach (var acc in businessAccounts)
            {
                Console.WriteLine($"Account Info: {acc}");
            }
            return businessAccounts;

        }

        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Business";
        }

        public override double GetIntrestrate()
        {
            return Interestrate = 0.03;
        }

    }
}
