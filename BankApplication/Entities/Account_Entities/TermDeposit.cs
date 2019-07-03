using System;
using System.Collections.Generic;
namespace Entities
{ 
    public class TermDeposit: Account, ITermDeposit
    {
        public TermDeposit(decimal termdepositamount)
        {
            Balance = termdepositamount;

        }

    
        private List<IAccount> termDepositAccounts = new List<IAccount>();

        DateTime maturityDateTime = new DateTime(2020,01,01); // add two year from currentdatetime
        DateTime currentDateTime = DateTime.Now;


        public override void OpenAccount(IAccount account)
        {
            termDepositAccounts.Add(account);

        }//end method OpenAccount()


        public override string GetAccountType(IAccount account)
        {
            return AccountType = "Term Deposit";
        }

        public override void Withdraw(IAccount account, decimal amount)
        {
            foreach (var acc in termDepositAccounts)
            {
                if (acc.Accountno == account.Accountno && acc.AccountStatus)
                {
                   if(maturityDateTime < currentDateTime)
                    {
                        acc.Balance -= amount;
                    }
                    else
                    {
                        Console.WriteLine($"You can not withdraw or transfer after {maturityDateTime}");
                    }

                }

                else
                {
                    Console.WriteLine("Record not found");
                }

            }

        } // end Withdraw

        public override double GetIntrestrate()
        {
            return Interestrate = 0.01;
        }

    }
}
